using System;
using System.Linq;
using System.Collections.Generic;
using Data.Entity;
using Data.Repositories;

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
        public int Add(Flight flight)
        {
            _dbContext.Flights.Add(flight);
            return _dbContext.SaveChanges();
        }
        public int Update(Flight flight)
        {
            _dbContext.Flights.Update(flight);
            return _dbContext.SaveChanges();
        }
        public int AddOrUpdate(Flight flight)
        {
            if(flight.Id == 0)
            {
                return this.Add(flight);
            }
            else
            {
                return this.Update(flight);
            }
        }
        public int Delete(Flight flight)
        {
            _dbContext.Flights.Remove(flight);
            return _dbContext.SaveChanges();
        }
    }
}