using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RealEstateAPI.API.Middleware;
using System.Net;
using System.Text;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class ExceptionMiddlewareTests
    {
        private Mock<RequestDelegate> _mockNext;
        private Mock<ILogger<ExceptionMiddleware>> _mockLogger;
        private ExceptionMiddleware _middleware;
        private DefaultHttpContext _httpContext;

        [SetUp]
        public void Setup()
        {
            _mockNext = new Mock<RequestDelegate>();
            _mockLogger = new Mock<ILogger<ExceptionMiddleware>>();
            _middleware = new ExceptionMiddleware(_mockNext.Object, _mockLogger.Object);
            _httpContext = new DefaultHttpContext();
            _httpContext.Response.Body = new MemoryStream();
        }

        [Test]
        public async Task InvokeAsync_WithNoException_ShouldCallNext()
        {
            // Arrange
            _mockNext.Setup(n => n(_httpContext)).Returns(Task.CompletedTask);

            // Act
            await _middleware.InvokeAsync(_httpContext);

            // Assert
            _mockNext.Verify(n => n(_httpContext), Times.Once);
            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task InvokeAsync_WithException_ShouldHandleException()
        {
            // Arrange
            var exception = new Exception("Test exception");
            _mockNext.Setup(n => n(_httpContext)).ThrowsAsync(exception);

            // Act
            await _middleware.InvokeAsync(_httpContext);

            // Assert
            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(500));
            Assert.That(_httpContext.Response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public async Task InvokeAsync_WithArgumentException_ShouldReturnInternalServerError()
        {
            // Arrange
            var exception = new ArgumentException("Invalid argument");
            _mockNext.Setup(n => n(_httpContext)).ThrowsAsync(exception);

            // Act
            await _middleware.InvokeAsync(_httpContext);

            // Assert
            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(500));
            Assert.That(_httpContext.Response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public async Task InvokeAsync_WithUnauthorizedAccessException_ShouldReturnInternalServerError()
        {
            // Arrange
            var exception = new UnauthorizedAccessException("Access denied");
            _mockNext.Setup(n => n(_httpContext)).ThrowsAsync(exception);

            // Act
            await _middleware.InvokeAsync(_httpContext);

            // Assert
            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(500));
            Assert.That(_httpContext.Response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public async Task InvokeAsync_WithFileNotFoundException_ShouldReturnInternalServerError()
        {
            // Arrange
            var exception = new FileNotFoundException("File not found");
            _mockNext.Setup(n => n(_httpContext)).ThrowsAsync(exception);

            // Act
            await _middleware.InvokeAsync(_httpContext);

            // Assert
            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(500));
            Assert.That(_httpContext.Response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public async Task InvokeAsync_WithNotImplementedException_ShouldReturnInternalServerError()
        {
            // Arrange
            var exception = new NotImplementedException("Not implemented");
            _mockNext.Setup(n => n(_httpContext)).ThrowsAsync(exception);

            // Act
            await _middleware.InvokeAsync(_httpContext);

            // Assert
            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(500));
            Assert.That(_httpContext.Response.ContentType, Is.EqualTo("application/json"));
        }
    }
}
