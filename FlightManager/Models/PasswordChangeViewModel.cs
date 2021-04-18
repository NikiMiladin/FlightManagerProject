using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
  
namespace FlightManager.Models
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        public string OldPassword { get; set;}
        [DataType(DataType.Password)]
        public string NewPassword { get; set;}

    }
}