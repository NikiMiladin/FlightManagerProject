using System;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username{ get; set;}
        [Required]
        [DataType(DataType.Password)]
        public string Password{ get; set;}

        public string ReturnUrl{ get; set;}
    }

}