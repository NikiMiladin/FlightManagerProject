using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models.Filters
{
    public class FlightsFilterViewModel
    {
        public string PilotName { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
    }
}
