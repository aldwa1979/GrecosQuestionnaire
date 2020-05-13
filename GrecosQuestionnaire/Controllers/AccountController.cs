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
using Microsoft.EntityFrameworkCore.Internal;

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
                var user = new IdentityUser { Email = model.Email, UserName = model.Email };
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
            AddViewBag();
            return View();
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

            foreach (var partner in _hotelRepository.GetPartners())
            {
                var partneruser = _hotelRepository.GetUsersPartners().Where(p => p.UserID == user.Id);

                foreach (var partuser in partneruser)
                {
                    if (partner.Id == partuser.PartnerModelID)
                    {
                        model.Partners.Add(partner.Name);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }

            else

            {
                user.UserName = model.Name;
                user.Email = model.Name;
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        public async Task<IActionResult> EditPartnerInUser(string userId)
        {
            ViewBag.UserID = userId;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {userId} cannot be found";
                return View("NotFound");
            }

            var partnersusers = _hotelRepository.GetUsersPartners().Where(p => p.UserID == user.Id).ToList();

            var partners = _hotelRepository.GetPartners();

            var model = new List<PartnerUserViewModel>();
            var model_true = new List<PartnerUserViewModel>();
            var model_false = new List<PartnerUserViewModel>();


            //wyszukuje wszystkich zaznaczonych partnerów
            foreach (var partner in partners)
            {
                foreach (var partneruser in partnersusers)
                {
                    var partnerUserViewModel = new PartnerUserViewModel
                    {
                        PartnerId = partner.Id.ToString(),
                        PartnerName = partner.Name
                    };

                    if (partner.Id == partneruser.PartnerModelID)
                    {
                        partnerUserViewModel.IsSelected = true;
                    }
                    else
                    {
                        partnerUserViewModel.IsSelected = false;
                    }

                    if (partnerUserViewModel.IsSelected == true)
                        model.Add(partnerUserViewModel);
                    else
                       continue;
                }
            }

            //wyszukuje wszystkich partnerów
            foreach (var partner in partners)
            {
                var partnerUserViewModel = new PartnerUserViewModel
                {
                    PartnerId = partner.Id.ToString(),
                    PartnerName = partner.Name,
                    IsSelected = false
                };

                model.Add(partnerUserViewModel);
            }

            //Usuwa duble - zostawia zaznaczonych
            return View(model.Distinct(new PartnerEquals()).ToList());
        }

        [HttpPost]
        public IActionResult EditPartnerInUser(List<PartnerUserViewModel> model, string userId)
        {
            List<int> partnerId = _hotelRepository.GetUsersPartners().Where(p => p.UserID == userId).Select(t => t.PartnerModelID).ToList();

            if (ModelState.IsValid)
            {
                foreach (var item in model)
                {
                    var y = Int32.Parse(item.PartnerId);
                    var x = !partnerId.Contains(y);

                    if (item.IsSelected == true && !(partnerId.Contains(Int32.Parse(item.PartnerId))))
                    {
                        var userpartner = new UserPartnerModel
                        {
                            PartnerModelID = Int32.Parse(item.PartnerId),
                            UserID = userId
                        };

                        _hotelRepository.UploadMatchUserPartner(userpartner);
                    }
                    else
                        continue;
                }
                return RedirectToAction("Index");
            }

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