using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class FlightViewModel
    {
        public int Id { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TimeSpan FlightDuration
        {
            get
            {
                return ArrivalTime.Subtract(DepartureTime);
            }
        }
        public int CapacityEconomyPassengers { get; set; }
        public int CapacityBusinessPassengers { get; set; }
    }
}
