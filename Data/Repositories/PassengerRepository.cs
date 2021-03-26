using System; 
using System.Linq;
using System.Collections.Generic;
using Data.Repositories;
using Data.Entity;

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
        public int Add(Passenger passenger)
        {
            _dbContext.Passengers.Add(passenger);
            _dbContext.Reservations.Add(passenger.Reservation);
            return _dbContext.SaveChanges();
        }
        public int Update(Passenger passenger)
        {
            _dbContext.Passengers.Update(passenger);
            return _dbContext.SaveChanges();

        }
        public int AddOrUpdate(Passenger passenger)
        {
            if(passenger.Id == 0)
            {
                return this.Add(passenger);
            }
            else
            {
                return this.Update(passenger);
            }
        }
        public int Delete(Passenger passenger)
        {
            _dbContext.Remove(passenger);
            return _dbContext.SaveChanges();
        }
    }
}