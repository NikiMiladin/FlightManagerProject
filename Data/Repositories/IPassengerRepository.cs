using System;
using System.Collections.Generic; 
using System.Linq;
using Data.Repositories;
using Data.Entity;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IPassengerRepository
    {
        IQueryable<Passenger> Items { get;}
        Task<int> Add(Passenger passenger);
        Task<int> Update(Passenger passenger);
        Task<int> AddOrUpdate(Passenger passenger);
        Task<int> Delete(Passenger passenger);
    }
}