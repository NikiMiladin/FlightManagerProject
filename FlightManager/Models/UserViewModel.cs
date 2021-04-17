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
        public string EGN { get; set;}
        public string Address { get; set;}
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }     
    }
}