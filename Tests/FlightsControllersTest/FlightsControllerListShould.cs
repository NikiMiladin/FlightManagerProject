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
    public class FlightsControllerListShould : FlightsControllerTestsBase
    {
        [Fact]
        public void ReturnAViewModelWithAListOfFlights()
        {
            // Arrange
            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlights());
            var mockReservationRepo = new Mock<IReservationRepository>();
            
            var controller = new FlightsController(mockFlightRepo.Object, mockReservationRepo.Object, _mapper);

            // Act
            var result = controller.List(new FlightAdminListViewModel());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<FlightAdminListViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Items.Count());
        }
    }
}
