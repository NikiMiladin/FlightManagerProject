using System; 
using System.Linq;
using System.Collections.Generic;
using Data.Repositories;
using Data.Entity;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly FlightDb _dbContext;

        public PassengerRepository (FlightDb dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Passenger> Items 
        {
            get
            {
                return _dbContext.Passengers;
            }
        }
        public async Task<int> Add(Passenger passenger)
        {
            await _dbContext.Passengers.AddAsync(passenger);
            // await _dbContext.Reservations.AddAsync(passenger.Reservation);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> Update(Passenger passenger)
        {
            _dbContext.Passengers.Update(passenger);
            return await _dbContext.SaveChangesAsync();

        }
        public async Task<int> AddOrUpdate(Passenger passenger)
        {
            if(passenger.Id == 0)
            {
                return await this.Add(passenger);
            }
            else
            {
                return await this.Update(passenger);
            }
        }
        public async Task<int> Delete(Passenger passenger)
        {
            _dbContext.Remove(passenger);
            return await _dbContext.SaveChangesAsync();
        }
    }
}