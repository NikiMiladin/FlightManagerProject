using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entity;
using Data.Repositories;
using FlightManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace FlightManager.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FlightsAdminController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IMapper _mapper;

        public FlightsAdminController(IFlightRepository flightRepository,IPassengerRepository passengerRepository, IMapper mapper)
        {
            _flightRepository = flightRepository;
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
            ICollection<Flight> flights = _flightRepository.Items
                                                        .OrderBy(item => item.Id)
                                                        .ToList();
            model.Items = _mapper.Map<ICollection<FlightAdminViewModel>>(flights);
            //flights.OrderBy(item => item.Id);
            /*model.Items = flights.Select(item => new FlightAdminViewModel()
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
            });*/
            return View(model);
        }
    }
}
