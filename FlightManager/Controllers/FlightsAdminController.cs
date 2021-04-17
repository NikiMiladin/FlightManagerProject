﻿using System;
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


namespace FlightManager.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FlightsAdminController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IPassengerRepository _passengerRepository;

        public FlightsAdminController(IFlightRepository flightRepository, IReservationRepository reservationRepository, IPassengerRepository passengerRepository)
        {
            _flightRepository = flightRepository;
            _reservationRepository = reservationRepository;
            _passengerRepository = passengerRepository;
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Flight flight =  _flightRepository.Items.SingleOrDefault(item => item.Id == id);
            FlightAdminViewModel model;
            if (flight != null)
            {
                model = new FlightAdminViewModel()
                {
                    Id = flight.Id,
                    DepartureCity = flight.DepartureCity,
                    ArrivalCity = flight.ArrivalCity,
                    DepartureTime = flight.DepartureTime,
                    ArrivalTime = flight.ArrivalTime,
                    PlaneModel = flight.PlaneModel,
                    PlaneID = flight.PlaneID,
                    PilotName = flight.PilotName,
                    CapacityEconomyPassengers = flight.CapacityEconomyPassengers,
                    CapacityBusinessPassengers = flight.CapacityBusinessPassengers
                };
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

            await _flightRepository.AddOrUpdate(new Flight()
            {
                Id = model.Id,
                DepartureCity = model.DepartureCity,
                ArrivalCity = model.ArrivalCity,
                DepartureTime = model.DepartureTime,
                ArrivalTime = model.ArrivalTime,
                PlaneModel = model.PlaneModel,
                PlaneID = model.PlaneID,
                PilotName = model.PilotName,
                CapacityEconomyPassengers = model.CapacityEconomyPassengers,
                CapacityBusinessPassengers = model.CapacityBusinessPassengers,
                Reservations = null
            });
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
                    TicketType = pass.TicketType })
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

            ReservationDetailsViewModel model = new ReservationDetailsViewModel()
            {
                DetailsAboutReservations = flight.Reservations.Select(re => new Models.ReservationsViewModel()
                { Id = re.Id, FlightId = re.FlightId, Email = re.Email,
                    PassengersEconomyCount = re.PassengersEconomyCount,
                    PassengersBusinessCount = re.PassengersBusinessCount })
             };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            Flight flight = _flightRepository.Items.SingleOrDefault(item => item.Id == id);
            if (flight == null)
            {
                return NotFound();
            }
            else
            {
                await _flightRepository.Delete(flight);
                return RedirectToAction("Index");
            }
        }
        [Authorize]
        public IActionResult Index(FlightIndexViewModel model)
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
        
        
            
    }
}
