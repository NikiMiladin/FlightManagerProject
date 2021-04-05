using System;
using System.Collections.Generic; 
using System.Linq;
using Data.Repositories;
using Data.Entity;

namespace Data.Repositories
{
    public interface IReservationRepository
    {
        IQueryable<Reservation> Items { get;}
        int Add(Reservation reservation);
        int Update(Reservation reservation);
        int AddOrUpdate(Reservation reservation);
        int Delete(Reservation reservation);
    }
}