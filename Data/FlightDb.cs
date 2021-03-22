﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Data.Entity;


namespace Data
{
    public class FlightDb : DbContext
    {
        public virtual DbSet<Flight> Flights {get; set;}
        public virtual DbSet<User> Users {get; set;}
        public virtual DbSet<Passenger> Passengers {get; set;}
        public virtual DbSet<Reservation> Reservations {get; set;}
            /*public virtual DbSet<ReservationPassenger> ReservationPassengers {get; set;}
            public virtual DbSet<FlightReservation> FlightReservations {get; set;}
            */  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Flight>()
                .HasMany(f => f.Reservations)
                .WithOne(r => r.Flight)
                .HasForeignKey(r => r.FlightId);
            modelBuilder.Entity<Reservation>()
                .HasMany(r => r.Passengers)
                .WithOne(p => p.Reservation)
                .HasForeignKey(p => p.ReservationId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=FlightDb;Integrated Security=True");
            builder.UseLazyLoadingProxies();
        }
    }
}
