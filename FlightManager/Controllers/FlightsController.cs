using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;
using Data.Repositories;
using FlightManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IReservationRepository _reservationRepository;

        public FlightsController(IFlightRepository flightRepository, IReservationRepository reservationRepository)
        {
            _flightRepository = flightRepository;
            _reservationRepository = reservationRepository;
        }
        public IActionResult List(FlightIndexViewModel model)
        {
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

            model.Items = flights.Select(item => new FlightAdminViewModel()
            {
                Id = item.Id,
                DepartureCity = item.DepartureCity,
                ArrivalCity = item.ArrivalCity,
                DepartureTime = item.DepartureTime,
                ArrivalTime = item.ArrivalTime,
                PlaneModel = item.PlaneModel,
                PlaneID = item.PlaneID,
                PilotName = item.PilotName,
                CapacityEconomyPassengers = item.CapacityEconomyPassengers,
                CapacityBusinessPassengers = item.CapacityBusinessPassengers
            });
            return View(model);
        }
        public IActionResult PassengersDetails(int id)
        {

            Reservation reservation = _reservationRepository.Items.SingleOrDefault(item => item.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            PassengerListViewModel model = new PassengerListViewModel()
            {
                Items = reservation.Passengers.Select(pass => new Models.PassengerViewModel()
                {
                    ReservationId = pass.ReservationId,
                    FirstName = pass.FirstName,
                    MiddleName = pass.MiddleName,
                    LastName = pass.LastName,
                    EGN = pass.EGN,
                    PhoneNumber = pass.PhoneNumber,
                    TicketType = pass.TicketType
                })
            };
            return View(model);
        }
        public IActionResult Details(int id) //details about reservations
        {

            Flight flight = _flightRepository.Items.SingleOrDefault(item => item.Id == id);
            if (flight == null)
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

            return View(model);
        }
       /* public IActionResult Index2(FlightListViewModel model)
        {
            model.Pager = model.Pager ?? new Models.PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Filter = model.Filter ?? new Models.Filters.FlightsFilterViewModel();

            bool emptyDepartureCity = string.IsNullOrWhiteSpace(model.Filter.DepartureCity);
            bool emptyArrivalCity = string.IsNullOrWhiteSpace(model.Filter.ArrivalCity);


            IQueryable<Flight> flights = _flightRepository.Items.Where(
                   item => (emptyDepartureCity || item.DepartureCity.Contains(model.Filter.DepartureCity)) 
                   && (emptyArrivalCity || item.ArrivalCity.Contains(model.Filter.ArrivalCity)));

            model.Pager.Pages = (int)Math.Ceiling((double)flights.Count() / model.Pager.ItemsPerPage);


            flights = flights.OrderBy(item => item.Id)
                .Skip((model.Pager.CurrentPage - 1) * model.Pager.ItemsPerPage)
                .Take(model.Pager.ItemsPerPage);



            model.Items = flights.Select(item => new FlightViewModel()
            {
                Id = item.Id,
                DepartureCity = item.DepartureCity,
                ArrivalCity = item.ArrivalCity,
                DepartureTime = item.DepartureTime,
                ArrivalTime = item.ArrivalTime,
               
            });
            return View(model);
        }*/
        public IActionResult Index(FlightListViewModel model)
        {

            model.Pager = model.Pager ?? new Models.PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Filter = model.Filter ?? new Models.Filters.FlightsFilterViewModel();

            bool emptyDepartureCity = string.IsNullOrWhiteSpace(model.Filter.DepartureCity);
            bool emptyArrivalCity = string.IsNullOrWhiteSpace(model.Filter.ArrivalCity);


            IQueryable<Flight> flights = _flightRepository.Items.Where(
                   item => (emptyDepartureCity || item.DepartureCity.Contains(model.Filter.DepartureCity))
                   && (emptyArrivalCity || item.ArrivalCity.Contains(model.Filter.ArrivalCity)));

            model.Pager.Pages = (int)Math.Ceiling((double)flights.Count() / model.Pager.ItemsPerPage);


            flights = flights.OrderBy(item => item.Id)
                .Skip((model.Pager.CurrentPage - 1) * model.Pager.ItemsPerPage)
                .Take(model.Pager.ItemsPerPage);
            model.Items = flights.Select(item => new FlightViewModel()
            {
                Id = item.Id,
                DepartureCity = item.DepartureCity,
                ArrivalCity = item.ArrivalCity,
                DepartureTime = item.DepartureTime,
                ArrivalTime = item.ArrivalTime
            }) ;
            return View(model);
        }
    }
}
