using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrecosQuestionnaire.Models;
using GrecosQuestionnaire.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GrecosQuestionnaire.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHotelRepository _hotelRepository;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IHotelRepository hotelRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _hotelRepository = hotelRepository;
        }

         public IActionResult Index()
        {
            var users = _userManager.Users;
            return View(users);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }

        public IActionResult Register()
        {
            AddViewBag();
            return View(new UserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            var userpartner = new UserPartnerModel();

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { Email = model.Email, UserName = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);

                userpartner.PartnerModelID = model.PartnersId.FirstOrDefault();
                userpartner.UserID = user.Id;
                _hotelRepository.UploadMatchUserPartner(userpartner);

                if (result.Succeeded)
                {
                    return RedirectToAction("index");
                }

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                Name = user.UserName
            };

            var partners = _hotelRepository.GetPartners();


            return View();
        }

        private void AddViewBag()
        {
            var partners = _hotelRepository.GetPartners().Select(x => new SelectListItem()
            {
                Text = x.Name.ToString(),
                Value = x.Id.ToString()
            }).ToList();

            //partners.Insert(0,
            //    new SelectListItem() { Selected = true, Text = string.Empty, Value = (-1).ToString(CultureInfo.InvariantCulture) });

            ViewBag.Partners = partners;
            //ViewBag.Owners = partners;

            //var users = Entity.Query<User>().Where(x => x.Partner.IsSpecial).Select(x => new SelectListItem()
            //{
            //    Selected = false,
            //    Text = x.Name,
            //    Value = x.Id.ToString(CultureInfo.InvariantCulture)
            //}).ToList();

            //ViewBag.Users = users;
        }
    }
}