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

        public FlightsController(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }
        public IActionResult List(FlightIndexViewModel model)
        {
            IQueryable<Flight> flights = _flightRepository.Items;
            flights.OrderBy(item => item.Id);
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
        public IActionResult Index(FlightListViewModel model)
        {
            IQueryable<Flight> flights = _flightRepository.Items;
            flights.OrderBy(item => item.Id);
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
