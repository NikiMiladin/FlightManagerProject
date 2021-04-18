using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using Data.Repositories;
using FlightManager.Controllers;
using FlightManager.Models;

namespace Tests.FlightsAdminControllerTests
{
    public class FlightsAdminControllerDeleteShould : FlightsAdminControllerTestsBase
    {
        [Fact]
        public void ReturnARedirectToActionResultWhenFlightIdIsIncorrect()
        {
            // Arrange
            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlights());
            var mockPassengerRepo = new Mock<IPassengerRepository>();
            var mockReservationRepo = new Mock<IReservationRepository>();
            var controller = new FlightsAdminController(mockFlightRepo.Object, mockReservationRepo.Object, mockPassengerRepo.Object, _mapper);
            int flightId = 3;

            // Act
            var result = controller.Delete(flightId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index",redirectToActionResult.ActionName);
        }
        
        [Fact]
        public void ReturnARedirectToActionResult()
        {
            // Arrange
            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlights());
            var mockPassengerRepo = new Mock<IPassengerRepository>();
            var mockReservationRepo = new Mock<IReservationRepository>();
            var controller = new FlightsAdminController(mockFlightRepo.Object, mockReservationRepo.Object, mockPassengerRepo.Object, _mapper);
            int flightId = 1;

            // Act
            var result = controller.Delete(flightId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result.Result);
            Assert.Equal("Index",redirectToActionResult.ActionName);
        }
    }
}