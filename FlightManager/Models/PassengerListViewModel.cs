using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class PassengerListViewModel
    {
        public IEnumerable<PassengerViewModel> Items { get; set; }
    }
}
