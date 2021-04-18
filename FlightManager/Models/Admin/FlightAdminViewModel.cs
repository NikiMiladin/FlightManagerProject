using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class FlightAdminViewModel : IValidatableObject
    {
        public int Id { get; set; }
        [Required]
        public string DepartureCity { get; set; }
        [Required]
        public string ArrivalCity { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        [Required]
        public DateTime ArrivalTime { get; set; }
        public TimeSpan FlightDuration
        {
            get
            {
                return ArrivalTime.Subtract(DepartureTime);
            }
        }
        [Required]
        public string PlaneModel { get; set; }
        [Required]
        public int PlaneID { get; set; }
        [Required]
        public string PilotName { get; set; }
        public int CapacityEconomyPassengers { get; set; }
        public int CapacityBusinessPassengers { get; set; }
        public ICollection<ReservationsViewModel> Reservations { get; set; }
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if(ArrivalTime<=DepartureTime)
            {
                yield return new ValidationResult("Arrival date and time must be greater than departure time and date!");
            }
            if(ArrivalTime<DateTime.Now)
            {
                yield return new ValidationResult("Invalid arrival time and date!");
            }
            if (DepartureTime < DateTime.Now)
            {
                yield return new ValidationResult("Invalid departure time and date!");
            }
            if(CapacityEconomyPassengers==0 && CapacityBusinessPassengers==0)
            {
                yield return new ValidationResult("Add seat capacity!");
            }
        }
    }
}
