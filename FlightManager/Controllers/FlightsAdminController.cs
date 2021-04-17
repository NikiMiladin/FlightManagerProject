using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;
using Data.Repositories;
using FlightManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FlightManager.Models.Details;
using FlightManager.Models.Utils;
using AutoMapper;


namespace FlightManager.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FlightsAdminController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IMapper _mapper;
        public FlightsAdminController(IFlightRepository flightRepository, IReservationRepository reservationRepository, IPassengerRepository passengerRepository, IMapper mapper)
        {
            _flightRepository = flightRepository;
            _reservationRepository = reservationRepository;
            _passengerRepository = passengerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Flight flight =  _flightRepository.Items.SingleOrDefault(item => item.Id == id);
            FlightAdminViewModel model;
            if (flight != null)
            {
                model = _mapper.Map<FlightAdminViewModel>(flight);
            }
            else
            {
                model = new FlightAdminViewModel();
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FlightAdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _flightRepository.AddOrUpdate(_mapper.Map<Flight>(model));
            return RedirectToAction("Index");
        }

        
        public IActionResult PassengersDetails(int id)
        {
          
            Reservation reservation = _reservationRepository.Items.SingleOrDefault(item => item.Id == id);
            if(reservation == null)
            {
                return NotFound();
            }
            PassengerListViewModel model = new PassengerListViewModel()
            {
                Items = reservation.Passengers.Select(pass => new Models.PassengerViewModel() { ReservationId = pass.ReservationId, FirstName = pass.FirstName,
                    MiddleName = pass.MiddleName, 
                    LastName = pass.LastName,
                EGN = pass.EGN, PhoneNumber = pass.PhoneNumber,
                    IsBusiness = pass.IsBusiness })
            };
            return View(model);
        }
        public IActionResult Details(int id) //details about reservations
        {
          
           Flight flight = _flightRepository.Items.SingleOrDefault(item => item.Id == id);
            if(flight == null)
            {
                return NotFound();
            }
            ReservationDetailsViewModel model = new ReservationDetailsViewModel();
            model.Pager = model.Pager ?? new Models.PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;
            model.Filter = model.Filter ?? new Models.Filters.ReservationsFilterViewModel();
            bool emptyEmail = string.IsNullOrWhiteSpace(model.Filter.Email);

            IQueryable<Reservation> reservations = flight.Reservations.Where(
                    item => (emptyEmail || item.Email.Contains(model.Filter.Email)) && (item.FlightId == id)).AsQueryable();

            model.Pager.Pages = (int)Math.Ceiling((double)reservations.Count() / model.Pager.ItemsPerPage);
            reservations = reservations.OrderBy(item => item.Id)
             .Skip((model.Pager.CurrentPage - 1) * model.Pager.ItemsPerPage)
              .Take(model.Pager.ItemsPerPage);
            model.DetailsAboutReservations = flight.Reservations.Select(item => new ReservationsViewModel()
            {
                Id = item.Id,
                FlightId = item.FlightId,
                Email = item.Email,
                PassengersEconomyCount = item.PassengersEconomyCount,
                PassengersBusinessCount = item.PassengersBusinessCount
            });
            /* ReservationDetailsViewModel model = new ReservationDetailsViewModel()
             {


                 DetailsAboutReservations = flight.Reservations.Select(re => new Models.ReservationsViewModel()
                 { Id = re.Id, FlightId = re.FlightId, Email = re.Email,
                     PassengersEconomyCount = re.PassengersEconomyCount,
                     PassengersBusinessCount = re.PassengersBusinessCount 
                 })
             };*/

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            Flight flight = _flightRepository.Items.SingleOrDefault(item => item.Id == id);
            if (flight == null)
            {
                //return NotFound();
                return RedirectToAction("Index"); //trqbva mi vtoro mnenie
            }
            else
            {
                await _flightRepository.Delete(flight);
                return RedirectToAction("Index");
            }
        }
        
        
        [Authorize]
        public IActionResult Index(FlightAdminListViewModel model)
        {
            //model.Pager ??= new PagerViewModel();
            model.Pager = model.Pager ?? new Models.PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;
           
            model.Filter = model.Filter ?? new Models.Filters.FlightsFilterViewModel();

            bool emptyPilotName = string.IsNullOrWhiteSpace(model.Filter.PilotName);


            IQueryable<Flight> flights = _flightRepository.Items.Where(
                   item => (emptyPilotName || item.PilotName.Contains(model.Filter.PilotName)));

            model.Pager.Pages = (int)Math.Ceiling((double)flights.Count() / model.Pager.ItemsPerPage);
           

            flights = flights.OrderBy(item => item.Id)
                .Skip((model.Pager.CurrentPage - 1) * model.Pager.ItemsPerPage)
                .Take(model.Pager.ItemsPerPage);
            
            model.Items = _mapper.Map<ICollection<FlightAdminViewModel>>(flights.ToList());
            return View(model);
        }
        
        
            
    }
}
