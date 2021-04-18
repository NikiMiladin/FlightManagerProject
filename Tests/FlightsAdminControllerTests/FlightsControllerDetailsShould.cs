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

namespace Tests.FlightsAdminControllerTests
{
    public class FlightsAdminControllerDetailsShould : FlightsAdminControllerTestsBase
    {
        [Fact]
        public void ReturnNotFoundWhenGivenAIncorrectFlightId()
        {
            // Arrange
            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlightWithReservations());
            var mockPassengerRepo = new Mock<IPassengerRepository>();
            var mockReservationRepo = new Mock<IReservationRepository>();
            var controller = new FlightsAdminController(mockFlightRepo.Object, mockReservationRepo.Object, mockPassengerRepo.Object, _mapper);
            int flightId = 3;

            // Act
            var result = controller.Details(flightId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void ReturnAViewResult()
        {
            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlightWithReservations());
            var mockPassengerRepo = new Mock<IPassengerRepository>();
            var mockReservationRepo = new Mock<IReservationRepository>();
            var controller = new FlightsAdminController(mockFlightRepo.Object, mockReservationRepo.Object, mockPassengerRepo.Object, _mapper);
            int flightId = 1;

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
