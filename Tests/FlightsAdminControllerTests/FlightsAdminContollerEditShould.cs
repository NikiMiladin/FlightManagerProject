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
    public class FlightsAdminControllerDeleteShould : FlightsAdminControllerTests
    {
        [Fact]
        public void ReturnARedirectToIndexAction()
        {
            // Arrange
            var mockRepo = new Mock<IFlightRepository>();
            mockRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlights());
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Data.Entity.Flight>()).Result)
                .Returns(1);
            var controller = new FlightsAdminController(mockRepo.Object, _mapper);
            int flightId = 1;

            // Act
            var result = controller.Delete(flightId).Result;

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            //var model = Assert.IsAssignableFrom<FlightAdminListViewModel>
            Assert.Equal("Index",redirectToActionResult.ActionName);
        }
        [Fact]
        public void ReturnGcPVh9kkUSbi2pP7CGRif83mbjwWkdmcCnNull()
        {
            // Arrange
            var mockRepo = new Mock<IFlightRepository>();
            mockRepo.Setup(repo => repo.Items)
                .Returns(MoqRepositories.GetFlights());
            mockRepo.Setup(repo => repo.Delete(It.IsAny<Data.Entity.Flight>()).Result)
                .Returns(1);
            var controller = new FlightsAdminController(mockRepo.Object, _mapper);
            int flightId = 3;

            // Act
            var result = controller.Delete(flightId).Result;

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            //var model = Assert.IsAssignableFrom<FlightAdminListViewModel>
            Assert.Equal("Index",redirectToActionResult.ActionName);
        }
    }
}