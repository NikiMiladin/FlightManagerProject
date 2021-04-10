using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Data.Entity;
using FlightManager.Models;

namespace FlightManager.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userMngr;
        private SignInManager<ApplicationUser> _signInMngr;

        public AccountController(UserManager<ApplicationUser> userMngr, SignInManager<ApplicationUser> signInMngr)
        {
            _signInMngr = signInMngr;
            _userMngr = userMngr;
        }

        public IActionResult Index()
        {
            return View();
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
                ApplicationUser applicationUser = await _userMngr.FindByNameAsync(login.Username);
                if (applicationUser != null)
                {
                    await _signInMngr.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInMngr.PasswordSignInAsync(applicationUser, login.Password, false, false);
                    if (result.Succeeded)
                        return Redirect(login.ReturnUrl ?? "/");
                }
                ModelState.AddModelError(nameof(login.Username), "Login Failed: Invalid Username or password");
            }
            return View(login);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInMngr.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}