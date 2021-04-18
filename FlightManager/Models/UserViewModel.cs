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
        [Required]
        public string UserName { get; set;}
        [Required]
        public string FirstName { get; set;}
        [Required]
        public string LastName { get; set;}
        public string FullName { get {return this.FirstName + " " + this.LastName;}}
        [Required]
        [MaxLength(10, ErrorMessage = "Max 10 digits")]
        public string EGN { get; set;}
        public string Address { get; set;}
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Max 10 digits")]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }     
        public bool IsEmployed {get; set;} 
        public bool IsAdmin {get; set;}
    }
}