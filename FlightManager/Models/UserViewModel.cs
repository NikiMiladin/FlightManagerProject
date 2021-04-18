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
        [Required(ErrorMessage = "Please enter a username.")]
        public string UserName { get; set;}
        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName { get; set;}
        [Required(ErrorMessage = "Please enter a last name.")]
        public string LastName { get; set;}
        public string FullName { get {return this.FirstName + " " + this.LastName;}}
        [Required(ErrorMessage = "Please enter an EGN.")]
        [MaxLength(10, ErrorMessage = "Max 10 digits")]
        public string EGN { get; set;}
        [Required(ErrorMessage = "Please enter an adress.")]
        public string Address { get; set;}
        [Required(ErrorMessage = "Please enter an email.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter a phone number")]
        [MaxLength(10, ErrorMessage = "Max 10 digits")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter a password.")]
        public string Password { get; set; }     
        public bool IsEmployed {get; set;} 
        public bool IsAdmin {get; set;}
    }
}