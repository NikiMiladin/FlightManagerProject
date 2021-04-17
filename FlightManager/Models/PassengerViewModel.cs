using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class PassengerViewModel
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsBusiness
        {
            get
            {
                if (TicketType == Ticket.Business)
                    return true;
                else return false;
            }
        }
        public Ticket TicketType { get; set; }
    }
    public enum Ticket
    {
        Business,
        Economy
    }
}
