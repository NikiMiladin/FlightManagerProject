using System;

namespace Data.Entity
{
    public class Passenger
    {
        public int ReservationId { get; set;} 
        public Reservation Reservation {get; set;}
        public string FirstName { get; set;}
        public string MiddleName { get; set;}
        public string LastName { get; set;}
        public int EGN { get; set;}
        public string PhoneNumber { get; set;}
        public bool TicketType { get; set;}
    }
}