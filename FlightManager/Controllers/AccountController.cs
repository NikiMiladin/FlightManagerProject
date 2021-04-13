using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Data.Entity;
using FlightManager.Models;
using AutoMapper;

namespace FlightManager.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IMapper _mapper;

        public AccountController(UserManager<ApplicationUser> userMngr, SignInManager<ApplicationUser> signInMngr,IMapper mapper)
        {
            _signInManager = signInMngr;
            _userManager = userMngr;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            if(!_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Home");
            }
            
            ApplicationUser user = await _userManager.GetUserAsync(User);
            UserViewModel model = _mapper.Map<UserViewModel>(user);
            return View(model);
        }

        public IActionResult Login(string returnUrl)
        {
            LoginViewModel login = new LoginViewModel();
            login.ReturnUrl = returnUrl;
            return View(login);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser applicationUser = await _userManager.FindByNameAsync(login.Username);
                if (applicationUser == null)
                {
                    ModelState.AddModelError(nameof(login.Username), "Login Failed: Invalid Username or password");
                }
                else if(!applicationUser.IsEmployed)
                {
                    ModelState.AddModelError("", "Your account has been deactivated, please contact an administrator.");
                }
                else
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(applicationUser, login.Password, false, false);
                    if (result.Succeeded)
                        return Redirect(login.ReturnUrl ?? "/");
                }
            }
            return View(login);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if(!ModelState.IsValid)
            {
               return RedirectToAction("Index", "Account");
            }
            IdentityResult result;
            ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                result = await _userManager.UpdateAsync(user);
                if(!result.Succeeded)
                {
                    AddErrors(result);
                }
            }
               return RedirectToAction("Index", "Account");
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(PasswordChangeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index","Account");
                }
                AddErrors(result);
                return View(model);
            }
                return RedirectToAction("Index","Home");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}