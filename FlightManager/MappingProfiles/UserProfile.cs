using System;
using AutoMapper;
using Data.Entity;
using FlightManager.Models;

namespace FlightManager.MappingProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser,UserViewModel>()
                .ReverseMap();
        }
    }

}