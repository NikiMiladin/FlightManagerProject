using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
  
namespace FlightManager.Models
{
    public class PasswordChangeViewModel
    {
        public string OldPassword { get; set;}
        public string NewPassword { get; set;}

    }
}