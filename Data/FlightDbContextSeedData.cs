using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Data.Entity;
namespace Data
{
    public static class FlightDbContextSeedData
    {
        public static void SeedAdmin(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager,string adminUsername,string adminPassword)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (userManager.GetUsersInRoleAsync("Administrator").Result.Count == 0)
            {
                ApplicationUser admin = new ApplicationUser(){
                    UserName = adminUsername,
                    IsEmployed = true
                    };
                userManager.CreateAsync(admin,adminPassword).Wait();
                userManager.AddToRoleAsync(admin,"Administrator").Wait();
            }
        }

    }
}