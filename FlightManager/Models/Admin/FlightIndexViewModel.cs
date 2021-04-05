using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class FlightIndexViewModel
    {
        public IQueryable<FlightAdminViewModel> Items { get; set; }
    }
}
