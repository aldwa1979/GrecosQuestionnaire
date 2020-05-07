using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrecosQuestionnaire.Models;
using GrecosQuestionnaire.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            return View(new UserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser { Email = model.Email, UserName = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);

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
    }
}