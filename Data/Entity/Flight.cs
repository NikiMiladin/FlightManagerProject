using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class Flight
    {
        public string Departure { get; set;}
        public string Arrival { get; set;}
        public string DepartureTime { get; set;}
        public string ArrivalTime { get; set;}
        public string PlaneModel { get; set;}
        public int PlaneID { get; set;}
        public string PilotName { get; set;}
        public string CapacityEconomyPassagers { get; set;}
        public string CapacityBusinessPassagers { get; set;}
        public ICollection<Reservation> Reservations { get; set;}
    }
    
}
