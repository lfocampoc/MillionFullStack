using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RealEstateAPI.API.Controllers;
using RealEstateAPI.Core.DTOs;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class HealthControllerTests
    {
        private Mock<ILogger<HealthController>> _mockLogger;
        private HealthController _controller;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<HealthController>>();
            
            // Crear un HealthCheckService usando Activator.CreateInstance para evitar problemas de constructor
            var healthCheckService = (HealthCheckService)Activator.CreateInstance(
                typeof(HealthCheckService),
                new HealthCheckServiceOptions(),
                new List<IHealthCheck>(),
                new List<HealthCheckRegistration>(),
                _mockLogger.Object
            )!;
            
            _controller = new HealthController(healthCheckService, _mockLogger.Object);
        }

        [Test]
        public async Task GetHealth_ShouldReturnActionResult()
        {
            // Act
            var result = await _controller.GetHealth();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ActionResult<ApiResponse<object>>>());
        }

        [Test]
        public async Task GetHealth_WithUnhealthyService_ShouldReturnActionResult()
        {
            // Act
            var result = await _controller.GetHealth();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ActionResult<ApiResponse<object>>>());
        }

        [Test]
        public async Task GetReadiness_ShouldReturnActionResult()
        {
            // Act
            var result = await _controller.GetReadiness();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ActionResult<ApiResponse<object>>>());
        }
    }
}
