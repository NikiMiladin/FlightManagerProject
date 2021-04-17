using System;
using AutoMapper;
using Data.Entity;
using FlightManager.Models;

namespace FlightManager.MappingProfile
{
    public class FlightProfile : Profile
    {
        public FlightProfile()
        {
            CreateMap<Flight,FlightViewModel>();
            CreateMap<Flight, FlightAdminViewModel>()
                .ReverseMap();
        }
    }

}