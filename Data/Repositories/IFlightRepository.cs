using System;
using System.Collections.Generic; 
using System.Linq;
using Data.Repositories;
using Data.Entity;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IFlightRepository
    {
        IQueryable<Flight> Items { get;}
        Task<int> Add(Flight Flight);
        Task<int> Update(Flight Flight);
        Task<int> AddOrUpdate(Flight Flight);
        Task<int> Delete(Flight Flight);
    }
}