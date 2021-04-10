using System;
using System.Linq;
using System.Collections.Generic;
using Data.Entity;
using Data.Repositories;

namespace Data.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly FlightDb _dbContext;
        public ReservationRepository(FlightDb dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Reservation> Items
        {
            get     
            {
                return _dbContext.Reservations;
            }      
        }
        public int Add(Reservation reservation)
        {
            _dbContext.Reservations.Add(reservation);
            if(reservation.Passengers!=null)
            {
                foreach (var passenger in reservation.Passengers)
                {
                    _dbContext.Passengers.Add(passenger);
                }
            }
            
            return _dbContext.SaveChanges();
        }
        public int Update(Reservation reservation)
        {
            _dbContext.Reservations.Update(reservation);
            return _dbContext.SaveChanges();
        }
        public int AddOrUpdate(Reservation reservation)
        {
            if(reservation.Id == 0)
            {
                return this.Add(reservation);
            }
            else
            {
                return this.Update(reservation);
            }
        }
        public int Delete(Reservation reservation)
        {
            _dbContext.Reservations.Remove(reservation);
            return _dbContext.SaveChanges();
        }
    }
}