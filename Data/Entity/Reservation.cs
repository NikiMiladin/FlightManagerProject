using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class Reservation
    {
        public int FlightId { get; set;}
        public Flight Flight{get; set;}
        public string Email { get; set;}
        public virtual ICollection<Passenger> Passengers {get; set;}
    }
}