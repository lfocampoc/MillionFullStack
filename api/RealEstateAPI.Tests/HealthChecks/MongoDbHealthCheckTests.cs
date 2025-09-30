using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using NUnit.Framework;
using RealEstateAPI.API.HealthChecks;
using MongoDB.Driver;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class MongoDbHealthCheckTests
    {
        private Mock<IMongoDatabase> _mockMongoDatabase;
        private MongoDbHealthCheck _healthCheck;

        [SetUp]
        public void Setup()
        {
            _mockMongoDatabase = new Mock<IMongoDatabase>();
            _healthCheck = new MongoDbHealthCheck(_mockMongoDatabase.Object);
        }

        [Test]
        public async Task CheckHealthAsync_WithHealthyDatabase_ShouldReturnHealthy()
        {
            // Arrange
            _mockMongoDatabase.Setup(db => db.RunCommandAsync(It.IsAny<Command<object>>(), null, default))
                .ReturnsAsync(new { ok = 1 });

            // Act
            var result = await _healthCheck.CheckHealthAsync(new HealthCheckContext(), default);

            // Assert
            Assert.That(result.Status, Is.EqualTo(HealthStatus.Healthy));
            Assert.That(result.Description, Is.EqualTo("MongoDB connection is working"));
        }

        [Test]
        public async Task CheckHealthAsync_WithUnhealthyDatabase_ShouldReturnUnhealthy()
        {
            // Arrange
            _mockMongoDatabase.Setup(db => db.RunCommandAsync(It.IsAny<Command<object>>(), null, default))
                .ThrowsAsync(new Exception("Database connection failed"));

            // Act
            var result = await _healthCheck.CheckHealthAsync(new HealthCheckContext(), default);

            // Assert
            Assert.That(result.Status, Is.EqualTo(HealthStatus.Unhealthy));
            Assert.That(result.Description, Is.EqualTo("MongoDB connection failed"));
        }
    }
}
