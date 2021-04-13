using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
  
namespace FlightManager.Models
{
    public class UserViewModel
    {
        public string Id {get; set;}
        public string UserName { get; set;}
        public string FirstName { get; set;}
        public string LastName { get; set;}
        public string FullName { get {return this.FirstName + " " + this.LastName;}}
        public string EGN { get; set;}
        public string Address { get; set;}   
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }    
        public bool IsEmployed {get; set;} 
        public bool IsAdmin {get; set;}
    }
}