using AutoMapper;
using Data.Entity;
using FlightManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightManager.MappingProfiles
{
    public class PassengerProfile : Profile
    {
        public PassengerProfile()
        {
            CreateMap<PassengerViewModel, Passenger>()
                .ReverseMap();
        }
    }
}
