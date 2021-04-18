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
    public class FlightsAdminControllerPassengerDetailsShould : FlightsAdminControllerTestsBase
    {
        [Fact]
        public void ReturnNotFoundWhenGivenAIncorrectReservationId()
        {
            // Arrange
            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlights());
            var mockPassengerRepo = new Mock<IPassengerRepository>();
            var mockReservationRepo = new Mock<IReservationRepository>();
            mockReservationRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetReservationWithPassengers().AsQueryable());
            var controller = new FlightsAdminController(mockFlightRepo.Object, mockReservationRepo.Object, mockPassengerRepo.Object, _mapper);
            int reservationId = 3;

            // Act
            var result = controller.PassengersDetails(reservationId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void ReturnAViewResult()
        {
            // Arrange
            var mockFlightRepo = new Mock<IFlightRepository>();
            mockFlightRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlights());
            var mockPassengerRepo = new Mock<IPassengerRepository>();
            var mockReservationRepo = new Mock<IReservationRepository>();
            mockReservationRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetReservationWithPassengers().AsQueryable());
            var controller = new FlightsAdminController(mockFlightRepo.Object, mockReservationRepo.Object, mockPassengerRepo.Object, _mapper);
            int reservationId = 1;
            // Act
            var result = controller.PassengersDetails(reservationId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<PassengerListViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal(3 ,model.Items.Count());
        }
    }
}
