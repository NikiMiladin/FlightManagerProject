using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;
using Data.Repositories;
using FlightManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FlightManager.Models.Details;

namespace FlightManager.Controllers
{
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
 
        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

       
        public IActionResult Index()
        {
            
            return View(_userManager.Users);
        }
        public ViewResult Create() => View();
       
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
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
                    if(_userManager.GetUsersInRoleAsync("Administrator").Result.Count() == 0)
                    {
                        result = await _userManager.AddToRoleAsync(appUser,"Administrator");
                        if(result.Succeeded)
                        {return RedirectToAction("Index");}
                        else
                        {
                            foreach (IdentityError error in result.Errors){
                                ModelState.AddModelError("", error.Description);
                                }
                        }                    
                    }
                    else 
                    {
                        await _userManager.AddToRoleAsync(appUser,"Employee");
                        if(result.Succeeded)
                        {return RedirectToAction("Index");}
                        else
                        {
                            foreach (IdentityError error in result.Errors){
                                ModelState.AddModelError("", error.Description);
                                }     
                        }   
                    }
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
        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            UserViewModel model;
            if(user != null)
            {
                model = new UserViewModel{
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EGN = user.EGN,
                    Address = user.Address,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };
            }
            else
            {
                model = new UserViewModel();
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            IdentityResult result;
            ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
            if(user != null)
            {
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.EGN = model.EGN;
                user.Address = model.Address;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                result = await _userManager.UpdateAsync(user);
                if(!result.Succeeded)
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                //else
                   // Errors(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", _userManager.Users);
        }
    }
}