using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class ReservationsViewModel
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
        public string Email { get; set; }
        public int PassengersEconomyCount { get; set; }
        public int PassengersBusinessCount { get; set; }
        public ICollection<PassengerViewModel> Passengers { get; set; }
    }
}
