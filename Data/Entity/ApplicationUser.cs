using System;
using Microsoft.AspNetCore.Identity;
namespace Data.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public int EGN { get; set;}
        public string Address { get; set;}
    }
    
}
