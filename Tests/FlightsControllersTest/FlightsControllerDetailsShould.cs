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
    public class FlightsControllerDetailsShould : FlightsControllerTestsBase
    {
        [Fact]
        public void ReturnNotFoundWhenGivenAIncorrectFlightId()
        {
            // Arrange
            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlights());
            var mockReservationRepo = new Mock<IReservationRepository>();
            int flightId = 3;
            var controller = new FlightsController(mockFlightRepo.Object, mockReservationRepo.Object, _mapper);

            // Act
            var result = controller.Details(flightId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void ReturnAViewResult()
        {
            // Arrange
            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlightWithReservations());
            var mockReservationRepo = new Mock<IReservationRepository>();
            int flightId = 1;
            var controller = new FlightsController(mockFlightRepo.Object, mockReservationRepo.Object, _mapper);

            // Act
            var result = controller.Details(flightId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ReservationDetailsViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(2,model.DetailsAboutReservations.Count());
        }
    }
}
