using System;
using AutoMapper;
using Data.Entity;
using FlightManager.Models;

namespace FlightManager.MappingProfile
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationsViewModel>();
        }
    }

}