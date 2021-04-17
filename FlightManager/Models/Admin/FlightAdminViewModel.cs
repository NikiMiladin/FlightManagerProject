using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class FlightAdminViewModel : FlightViewModel
    {
        public string PlaneModel { get; set; }
        public int PlaneID { get; set; }
        public string PilotName { get; set; }
        public int CapacityEconomyPassengers { get; set; }
        public int CapacityBusinessPassengers { get; set; }
        public ICollection<ReservationsViewModel> Reservations { get; set; }
    }
}
