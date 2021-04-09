using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;
using Data.Repositories;
using FlightManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;


namespace FlightManager.Controllers
{
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
 
        public UserController(UserManager<ApplicationUser> usrMgr)
        {
            _userManager = usrMgr;
        }
 
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }
        public ViewResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser{
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EGN = user.EGN,
                    Address = user.Address,
                    Email= user.Email,
                    PhoneNumber = user.PhoneNumber
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors){
                        ModelState.AddModelError("", error.Description);
                        }
                }
            }
            return View(user);
        }
    }
}