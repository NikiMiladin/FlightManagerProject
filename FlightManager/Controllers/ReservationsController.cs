using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Entity;
using Data.Repositories;
using FlightManager.Models;
using FlightManager.Models.Details;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IPassengerRepository _passengerRepository;

        public ReservationsController(IFlightRepository flightRepository, IReservationRepository reservationRepository, IPassengerRepository passengerRepository)
        {
            _flightRepository = flightRepository;
            _reservationRepository = reservationRepository;
            _passengerRepository = passengerRepository;
        }
        [HttpGet]
        public IActionResult Add(int id)
        {
            ReservationsViewModel model = new ReservationsViewModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ReservationsViewModel model)
        {
            if(ModelState.IsValid)
            {
                Flight flight = _flightRepository.Items.SingleOrDefault(item => item.Id == model.Id);
                model.FlightId = flight.Id;
                model.Flight = flight;
                if (model.Flight.CapacityEconomyPassengers >= model.PassengersEconomyCount && model.Flight.CapacityBusinessPassengers >= model.PassengersBusinessCount)
                {
                    flight.CapacityEconomyPassengers -= model.PassengersEconomyCount;
                    flight.CapacityBusinessPassengers -= model.PassengersBusinessCount;
                    
                    Reservation reservation = new Reservation()
                    {
                        FlightId = model.FlightId,
                        Flight = flight,
                        Email = model.Email,
                        PassengersEconomyCount = model.PassengersEconomyCount,
                        PassengersBusinessCount = model.PassengersBusinessCount,
                        Passengers=null
                    };
                    
                    await _reservationRepository.Add(reservation);
                    //flight.Reservations.Add(reservation);
                    await _flightRepository.Update(flight);
                    return RedirectToAction("AddPassengers", reservation);
                }
                else
                {
                    ModelState.AddModelError("","There isn't enough space");
                    //да изпишем на потребителя че няма толкова и такива свободни места, каквито желае
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddPassengers(Reservation reservation)
        {
            PassengerViewModel model = new PassengerViewModel();
            return View(model);
            
        }
        [HttpPost]
        public async Task<IActionResult> AddPassengers(PassengerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Reservation reservation = _reservationRepository.Items.SingleOrDefault(item => item.Id == model.Id);
                model.Reservation = reservation;
                model.ReservationId = reservation.Id;
                int businessCount = reservation.PassengersBusinessCount;
                int economyCount = reservation.PassengersEconomyCount;
                Passenger passenger = new Passenger
                {
                    ReservationId = model.ReservationId,
                    Reservation = model.Reservation,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    EGN = model.EGN,
                    PhoneNumber = model.PhoneNumber,
                    IsBusiness = model.IsBusiness
                };
                await _passengerRepository.Add(passenger);
                reservation.Passengers.Add(passenger);
                await _reservationRepository.Update(reservation);
                if(reservation.Passengers.Count==(economyCount+businessCount))
                    return RedirectToAction("Index", "Flights");
                // return View("./Index", "FlightList");
                return RedirectToAction("AddPassengers", reservation);
            }
            return RedirectToAction("Index", "Flights");
            //return View("Index","FlightList");
        }
      /*  public IActionResult ResDetailsIndex(ReservationDetailsViewModel model)
        {
            model.Pager = model.Pager ?? new Models.PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Filter = model.Filter ?? new Models.Filters.ReservationsFilterViewModel();
            bool emptyEmail = string.IsNullOrWhiteSpace(model.Filter.Email);


            IQueryable<Reservation> reservations = _reservationRepository.Items.Where(
                    item => (emptyEmail || item.Email.Contains(model.Filter.Email)));

            model.Pager.Pages = (int)Math.Ceiling((double)reservations.Count() / model.Pager.ItemsPerPage);
            reservations = reservations.OrderBy(item => item.Id)
             .Skip((model.Pager.CurrentPage - 1) * model.Pager.ItemsPerPage)
              .Take(model.Pager.ItemsPerPage);
            model.DetailsAboutReservations = reservations.Select(item => new ReservationsViewModel()
            {
                Id = item.Id,
                FlightId = item.FlightId,
                Email = item.Email,
                PassengersEconomyCount = item.PassengersEconomyCount,
                PassengersBusinessCount = item.PassengersBusinessCount

            });
            return View(model);
        }*/
       
            public IActionResult Index(ReservationDetailsViewModel model)
            {
            model.Pager = model.Pager ?? new Models.PagerViewModel();
            model.Pager.CurrentPage = model.Pager.CurrentPage <= 0 ? 1 : model.Pager.CurrentPage;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Filter = model.Filter ?? new Models.Filters.ReservationsFilterViewModel();
            bool emptyEmail = string.IsNullOrWhiteSpace(model.Filter.Email);


            IQueryable<Reservation> reservations = _reservationRepository.Items.Where(
                    item => (emptyEmail || item.Email.Contains(model.Filter.Email)));

            model.Pager.Pages = (int)Math.Ceiling((double)reservations.Count() / model.Pager.ItemsPerPage);
            reservations = reservations.OrderBy(item => item.Id)
             .Skip((model.Pager.CurrentPage - 1) * model.Pager.ItemsPerPage)
              .Take(model.Pager.ItemsPerPage);
            model.DetailsAboutReservations = reservations.Select(item => new ReservationsViewModel()
            {
                Id = item.Id,
                FlightId = item.FlightId,
                Email = item.Email,
                PassengersEconomyCount = item.PassengersEconomyCount,
                PassengersBusinessCount = item.PassengersBusinessCount

            });
            return View(model);
            }
    }
}
