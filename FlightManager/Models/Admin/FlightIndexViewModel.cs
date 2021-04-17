using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Models.Base;
using FlightManager.Models.Filters;

namespace FlightManager.Models
{
    public class FlightIndexViewModel : BaseIndexViewModel
    {
        public IQueryable<FlightAdminViewModel> Items { get; set; }
        public FlightsFilterViewModel Filter { get; set; }
    }
}
