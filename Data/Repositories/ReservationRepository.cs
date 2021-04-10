using System;
using System.Linq;
using System.Collections.Generic;
using Data.Entity;
using Data.Repositories;
using System.Threading.Tasks;

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
        public async Task<int> Add(Reservation reservation)
        {
            await _dbContext.Reservations.AddAsync(reservation);
            if(reservation.Passengers!=null)
            {
                foreach (var passenger in reservation.Passengers)
                {
                    await _dbContext.Passengers.AddAsync(passenger);
                }
            }
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> Update(Reservation reservation)
        {
            _dbContext.Reservations.Update(reservation);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> AddOrUpdate(Reservation reservation)
        {
            if(reservation.Id == 0)
            {
                return await this.Add(reservation);
            }
            else
            {
                return await this.Update(reservation);
            }
        }
        public async Task<int> Delete(Reservation reservation)
        {
            _dbContext.Reservations.Remove(reservation);
            return await _dbContext.SaveChangesAsync();
        }
    }
}