using System;
using System.Linq;
using System.Collections.Generic;
using Data.Entity;

namespace Tests
{
    public static class MoqRepositories
    {
        public static IQueryable<Flight> GetFlights()
        {
            var flights = new List<Flight>();
            flights.Add(new Flight()
            {
                Id = 1,
                DepartureCity = "Burgas",
                ArrivalCity = "Sofia",
                DepartureTime = new DateTime(2020,08,20),
                ArrivalTime = new DateTime(2020,08,21),
                PlaneModel = "Airbus A380",
                PlaneID = 1,
                PilotName = "Ivan",
                CapacityEconomyPassengers = 20,
                CapacityBusinessPassengers = 10
            });
            flights.Add(new Flight()
            {
                Id = 2,
                DepartureCity = "Sofia",
                ArrivalCity = "Burgas",
                DepartureTime = new DateTime(2020,08,22),
                ArrivalTime = new DateTime(2020,08,23),
                PlaneModel = "Airbus A270",
                PlaneID = 2,
                PilotName = "Petar",
                CapacityEconomyPassengers = 20,
                CapacityBusinessPassengers = 10                
            });
            return flights.AsQueryable();
        }

        public static IQueryable<Flight> GetFlightWithReservations()
        {
            var flights = new List<Flight>();
            flights.Add(new Flight()
            {
                Id = 1,
                DepartureCity = "Burgas",
                ArrivalCity = "Sofia",
                DepartureTime = new DateTime(2020,08,20),
                ArrivalTime = new DateTime(2020,08,21),
                PlaneModel = "Airbus A380",
                PlaneID = 1,
                PilotName = "Ivan",
                CapacityEconomyPassengers = 20,
                CapacityBusinessPassengers = 10
                }
            );
            flights[0].Reservations = MoqRepositories.GetReservations(1, flights[0]);
            return flights.AsQueryable();
        }
        public static ICollection<Reservation> GetReservations(int flightId, Flight flight)
        {
            List<Reservation> reservations = new List<Reservation>();
            reservations.Add(new Reservation{
                Id = 1,
                FlightId = flightId,
                Flight = flight, 
                Email = "fake@email.com",
                PassengersBusinessCount = 1,
                PassengersEconomyCount = 0,
                Passengers = new List<Passenger>()
            });
            reservations.Add(new Reservation{
                Id = 2,
                FlightId = flightId,
                Flight = flight, 
                Email = "anotherFake@email.com",
                PassengersBusinessCount = 0,
                PassengersEconomyCount = 1,
                Passengers = new List<Passenger>()
            });
            return reservations;
        }
        public static ICollection<Reservation> GetReservationWithPassengers()
        {
            Reservation reservation = new Reservation{
                Id = 1,
                FlightId = 1,
                Email = "fake@email.com",
                PassengersBusinessCount = 2,
                PassengersEconomyCount = 1,
                Passengers = new List<Passenger>()
            };
            reservation.Passengers.Add(new Passenger{
                Id = 1,
                ReservationId = reservation.Id,
                FirstName = "Ivan",
                MiddleName = "Petrov",
                LastName = "Petrov",
                EGN = "123456789",
                PhoneNumber = "088 123 4567",
                IsBusiness = true
            });
            reservation.Passengers.Add(new Passenger{
                Id = 2,
                ReservationId = reservation.Id,
                FirstName = "Georgi",
                MiddleName = "Petrov",
                LastName = "Petrov",
                EGN = "159786324",
                PhoneNumber = "088 245 4577",
                IsBusiness = true
            });
            reservation.Passengers.Add(new Passenger{
                Id = 3,
                ReservationId = reservation.Id,
                FirstName = "Petar",
                MiddleName = "Petrov",
                LastName = "Petrov",
                EGN = "845697212",
                PhoneNumber = "088 123 7787",
                IsBusiness = false
            });
            return new List<Reservation>{reservation};
        }
    }
}