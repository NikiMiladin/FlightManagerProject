using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Models.Base;

namespace FlightManager.Models.Details
{
    public class UserDetailsViewModel : BaseIndexViewModel
    {
        public IEnumerable<UserViewModel> DetailsAboutUsers { get; set; }
    }
}
