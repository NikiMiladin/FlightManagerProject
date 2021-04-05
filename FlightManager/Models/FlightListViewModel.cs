using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class FlightListViewModel
    {
        public IQueryable<FlightViewModel> Items { get; set; }
    }
}
