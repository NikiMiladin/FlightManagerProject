using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.Models
{
    public class FlightAdminViewModel : IValidatableObject
    {
        public string PlaneModel { get; set; }
        public int PlaneID { get; set; }
        public string PilotName { get; set; }
        public int CapacityEconomyPassengers { get; set; }
        public int CapacityBusinessPassengers { get; set; }
        public ICollection<ReservationsViewModel> Reservations { get; set; }
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if(ArrivalTime<DepartureTime)
            {
                yield return new ValidationResult("Arrival date and time must be greater than departure time and date!");
            }
            if(ArrivalTime<DateTime.Now)
            {
                yield return new ValidationResult("Invalid arrival time and date!");
            }
            if (DepartureTime.Date < DateTime.Now.Date)
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
