using System;
using System.Collections.Generic; 
using System.Linq;
using Data.Repositories;
using Data.Entity;

namespace Data.Repositories
{
    public interface IPassengerRepository
    {
        IQueryable<Passenger> Items { get;}
        int Add(Passenger passenger);
        int Update(Passenger passenger);
        int AddOrUpdate(Passenger passenger);
        int Delete(Passenger passenger);
    }
}