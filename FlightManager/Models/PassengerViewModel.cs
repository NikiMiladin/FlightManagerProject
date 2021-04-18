using Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class PassengerViewModel
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [MaxLength(10,ErrorMessage = "Max 10 digits")]
        public string EGN { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage ="Max 10 digits")]
        public string PhoneNumber { get; set; }
        public bool IsBusiness
        {
            get
            {
                if (TicketType == Ticket.Business)
                    return true;
                else return false;
            }
            set
            {
                if(value==true)
                {
                    TicketType = Ticket.Business;
                }
                else TicketType = Ticket.Economy;
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
