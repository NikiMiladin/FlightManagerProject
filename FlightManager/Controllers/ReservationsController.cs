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
    public class ReservationsController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private static IFlightRepository _flightRepository;
        private readonly IPassengerRepository _passengerRepository;

        public ReservationsController(IReservationRepository reservationRepository,
            IFlightRepository flightRepository, IPassengerRepository passengerRepository)
        {
            _reservationRepository = reservationRepository;
            _flightRepository = flightRepository;
            _passengerRepository = passengerRepository;
        }
        [HttpGet]
        public IActionResult Add(int flightId)
        {
            Flight flight = _flightRepository.Items.SingleOrDefault(item => item.Id == flightId);
            ReservationsViewModel model = new ReservationsViewModel();
            model.FlightId = flight.Id;
            model.Flight = flight;
            /*{
                FlightId = flight.Id,
                Flight = flight,
                Email = "",
                PassengersCount = 0,
                Passengers = null
            };*/
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(ReservationsViewModel model)
        {
            if(ModelState.IsValid)
            {
                Reservation reservation = new Reservation()
                {
                    Id = model.Id,
                    FlightId = model.FlightId,
                    Flight = model.Flight,
                    Email = model.Email,
                    PassengersCount = model.PassengersCount
                };
                _reservationRepository.Add(reservation);
                return RedirectToAction("AddPassengers", reservation.Id);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddPassengers(int reservationId)
        {
            Reservation reservation = _reservationRepository.Items.SingleOrDefault(item => item.Id == reservationId);
            PassengerViewModel model = new PassengerViewModel()
            {
                ReservationId = reservation.Id,
                Reservation = reservation
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddPassengers(PassengerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Passenger passenger = new Passenger
                {
                    Id = model.Id,
                    ReservationId = model.ReservationId,
                    Reservation = model.Reservation,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    EGN = model.EGN,
                    PhoneNumber = model.PhoneNumber,
                    TicketType = model.TicketType
                };
                _passengerRepository.Add(passenger);
            }//kolko puti i kak shte se izpulnqva, nakude da vodi???
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
