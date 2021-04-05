using System;
using System.Collections.Generic;

namespace Data.Entity
{
    public class Flight
    {
        public int Id { get; set;}
        public string DepartureCity { get; set;}
        public string ArrivalCity { get; set;}
        public DateTime DepartureTime { get; set;}
        public DateTime ArrivalTime { get; set;}
        public string PlaneModel { get; set;}
        public int PlaneID { get; set;}
        public string PilotName { get; set;}
        public int CapacityEconomyPassengers { get; set;}
        public int CapacityBusinessPassengers { get; set;}
        public virtual ICollection<Reservation> Reservations { get; set;}
    }
    
}
