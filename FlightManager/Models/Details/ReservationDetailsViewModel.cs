using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Models.Base;
using FlightManager.Models.Filters;

namespace FlightManager.Models
{
    public  class ReservationDetailsViewModel : BaseIndexViewModel
    {
        // v Views/FlightsAdmin/Details pokazwa detailna informaciq za rezervaciite toest koi putnici sa w rezerwaciqta
        public IEnumerable<ReservationsViewModel> DetailsAboutReservations { get; set; }
        public ReservationsFilterViewModel Filter { get; set; }
        
    }
}
