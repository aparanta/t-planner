using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using TruckPlanner.Controllers;
using TruckPlanner.DTO;

namespace TruckPlanner.Tests.Controllers
{
    [TestClass]
    public class TruckPlanControllerTests
    {
        private TruckPlanController _controller;

        [TestInitialize]
        public void Setup()
        {
            var logger = new LoggerFactory().CreateLogger<TruckPlanController>();
            _controller = new TruckPlanController(logger);
        }

        [TestMethod]
        public void CalculateDistance_ReturnsBadRequest_WhenTruckPlanIsNull()
        {
            // Act
            var result = _controller.CalculateDistance(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void CalculateDistance_ReturnsBadRequest_WhenPositionsIsNull()
        {
            // Arrange
            var truckPlan = new TruckPlan { Positions = null };

            // Act
            var result = _controller.CalculateDistance(truckPlan);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void CalculateDistance_ReturnsBadRequest_WhenPositionsCountLessThan2()
        {
            // Arrange
            var truckPlan = new TruckPlan
            {
                Positions = new List<GpsPosition>
                {
                    new GpsPosition { Latitude = 0, Longitude = 0, Timestamp = DateTime.UtcNow }
                }
            };

            // Act
            var result = _controller.CalculateDistance(truckPlan);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void CalculateDistance_ReturnsOk_WithCorrectDistance()
        {
            // Arrange
            var truckPlan = new TruckPlan
            {
                Positions = new List<GpsPosition>
                {
                    new GpsPosition { Latitude = 0, Longitude = 0, Timestamp = DateTime.UtcNow },
                    new GpsPosition { Latitude = 0, Longitude = 1, Timestamp = DateTime.UtcNow.AddMinutes(1) }
                }
            };

            // Act
            var result = _controller.CalculateDistance(truckPlan) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            var value = result.Value as DistanceResponse;
            Assert.IsNotNull(value);
            double distance = value.DistanceKm;
            // The distance between (0,0) and (0,1) is about 111.19 km
            Assert.IsTrue(distance > 110 && distance < 112);
        }
    }
}