using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrecosQuestionnaire.Logic.Hotels;
using GrecosQuestionnaire.Models;
using GrecosQuestionnaire.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using NETCore.MailKit.Core;

namespace GrecosQuestionnaire.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHotelRepository _hotelRepository;
        private readonly IEmailService _emailService;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IHotelRepository hotelRepository, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _hotelRepository = hotelRepository;
            _emailService = emailService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
                return View("ResetPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null )//&& await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword","Account", 
                        new {email = model.Email, token = token}, Request.Scheme);

                    //logger.Log(LogLevel.Warning, passwordResetLink);

                    return View("ForgotPasswordConfirmation");
                }
                return View("ForgotPasswordConfirmation");
            }
            return View(model);
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

                var user = await _userManager.FindByEmailAsync(model.Email);

                if ( user != null && !user.EmailConfirmed && 
                                    (await _userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "myhotels");
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
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                //userpartner.PartnerModelID = model.PartnersId.FirstOrDefault();
                //userpartner.UserID = user.Id;
                //_hotelRepository.UploadMatchUserPartner(userpartner);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme, "www.grecos.pl");

                    await _emailService.SendAsync(model.Email, "email test", $"<a href=\"{confirmationLink}\">Confirm Email</a>", true);

                    if (_signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index", "Administration");
                    }

                    ViewBag.ErrorTitle = "Registration successful";
                    return View("Error");
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            AddViewBag();
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail (string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return View();
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
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
                    if (item.IsSelected == true && !(partnerId.Contains(Int32.Parse(item.PartnerId))))
                    {
                        var userpartner = new UserPartnerModel
                        {
                            PartnerModelID = Int32.Parse(item.PartnerId),
                            UserID = userId
                        };

                        _hotelRepository.UploadMatchUserPartner(userpartner);
                    }
                    else if (item.IsSelected == false && (partnerId.Contains(Int32.Parse(item.PartnerId))))
                    {
                        var id = _hotelRepository.GetUsersPartners().Where(p => p.PartnerModelID == Int32.Parse(item.PartnerId) && p.UserID == userId).Select(a => a.Id).FirstOrDefault();
                        _hotelRepository.RemoveMatchUserPartner(id);
                    }
                    else
                        continue;
                }
                return RedirectToAction("Edit", new { Id = userId });
            }

            return RedirectToAction("Edit", new { Id = userId });
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