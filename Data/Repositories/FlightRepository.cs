using System;
using System.Linq;
using System.Collections.Generic;
using Data.Entity;
using Data.Repositories;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightDb _dbContext;
        public FlightRepository(FlightDb dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Flight> Items
        {
            get     
            {
                return _dbContext.Flights;
            }      
        }
        public async Task<int> Add(Flight flight)
        {
            await _dbContext.Flights.AddAsync(flight);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> Update(Flight flight)
        {
            _dbContext.Flights.Update(flight);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> AddOrUpdate(Flight flight)
        {
            if(flight.Id == 0)
            {
                return await this.Add(flight);
            }
            else
            {
                return await this.Update(flight);
            }
        }
        public async Task<int> Delete(Flight flight)
        {
            _dbContext.Flights.Remove(flight);
            return await _dbContext.SaveChangesAsync();
        }
    }
}