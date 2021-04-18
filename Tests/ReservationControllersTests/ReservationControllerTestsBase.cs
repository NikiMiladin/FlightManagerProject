using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using AutoMapper;
using Data.Repositories;
using FlightManager.Controllers;
using FlightManager.MappingProfile;
using FlightManager.Models;

namespace Tests.FlightsControllerTests
{
    public abstract class ReservationControllerTestsBase
    {
        protected static IMapper _mapper;
 
        public ReservationControllerTestsBase()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new FlightProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }
    }
}
