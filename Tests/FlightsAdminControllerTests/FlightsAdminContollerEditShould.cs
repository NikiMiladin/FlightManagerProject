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
    public class FlightsAdminControllerEditShould : FlightsAdminControllerTestsBase
    {
        [Fact]
        public void ReturnAViewResult()
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
            var result = controller.Edit(flightId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<FlightAdminViewModel>(viewResult.ViewData.Model);
            Assert.NotNull(model.ArrivalCity);       
        }
        [Fact]
        public void ReturnAViewResultWithAnEmptyModelWhenFlightIdIsIncorrect()
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
            var result = controller.Edit(flightId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<FlightAdminViewModel>(viewResult.ViewData.Model);
            Assert.Null(model.ArrivalCity);       
        }
        [Fact]
        public void ReturnAViewResultWhenModelStateIsInvalid()
        {
            // Arrange
            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlights());
            var mockPassengerRepo = new Mock<IPassengerRepository>();
            var mockReservationRepo = new Mock<IReservationRepository>();
            var controller = new FlightsAdminController(mockFlightRepo.Object, mockReservationRepo.Object, mockPassengerRepo.Object, _mapper);
    	    controller.ModelState.AddModelError("test", "test");
            // Act
            var result = controller.Edit(new FlightAdminViewModel()).Result;

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<FlightAdminViewModel>(viewResult.ViewData.Model);       
        }
        [Fact]
        public void ReturnARedirectToActionResultWhenModelStateIsValid()
        {
            // Arrange
            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlights());
            var mockPassengerRepo = new Mock<IPassengerRepository>();
            var mockReservationRepo = new Mock<IReservationRepository>();
            var controller = new FlightsAdminController(mockFlightRepo.Object, mockReservationRepo.Object, mockPassengerRepo.Object, _mapper);
    	    controller.ModelState.Clear();
            // Act
            var result = controller.Edit(new FlightAdminViewModel()).Result;

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index",redirectToActionResult.ActionName);
        }
    }
}