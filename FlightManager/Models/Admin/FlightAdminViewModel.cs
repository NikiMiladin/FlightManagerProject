using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class FlightAdminViewModel
    {
        public int Id { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string PlaneModel { get; set; }
        public int PlaneID { get; set; }
        public string PilotName { get; set; }
        public int CapacityEconomyPassengers { get; set; }
        public int CapacityBusinessPassengers { get; set; }
        public TimeSpan FlightDuration {
            get
            {
                return ArrivalTime.Subtract(DepartureTime);
            }
        }
        public ICollection<ReservationsViewModel> Reservations { get; set; }
    }
}
