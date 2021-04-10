using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Data.Entity;
namespace Data
{
    public static class FlightDbContextSeedData
    {

/*        FlightDb _dbContext;

        RoleManager<IdentityRole> _roleManager;

        public FlightDbContextSeedData(FlightDb dbContext, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }
        
        public async void SeedRoles()
        {
            IdentityResult result;
            if( _roleManager.Roles.ElementType)
            {
               result = await _roleManager.CreateAsync(new IdentityRole("Administrator"));
            }
            if(!await _roleManager.RoleExistsAsync("Employee"))
            {
               await _roleManager.CreateAsync(new IdentityRole("Employee"));
            }
            await _dbContext.SaveChangesAsync();

        }*/
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Employee";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

    }
}