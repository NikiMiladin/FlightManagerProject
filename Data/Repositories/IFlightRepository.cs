using System;
using System.Collections.Generic; 
using System.Linq;
using Data.Repositories;
using Data.Entity;

namespace Data.Repositories
{
    interface IFlightRepository
    {
        IQueryable<Flight> Items { get;}
        int Add(Flight Flight);
        int Update(Flight Flight);
        int AddOrUpdate(Flight Flight);
        int Delete(Flight Flight);
    }
}