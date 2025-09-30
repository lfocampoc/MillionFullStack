using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RealEstateAPI.API.Middleware;
using System.Text;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class ResponseMiddlewareTests
    {
        private Mock<RequestDelegate> _mockNext;
        private Mock<ILogger<ResponseMiddleware>> _mockLogger;
        private ResponseMiddleware _middleware;
        private DefaultHttpContext _httpContext;

        [SetUp]
        public void Setup()
        {
            _mockNext = new Mock<RequestDelegate>();
            _mockLogger = new Mock<ILogger<ResponseMiddleware>>();
            _middleware = new ResponseMiddleware(_mockNext.Object, _mockLogger.Object);
            _httpContext = new DefaultHttpContext();
            // No asignar stream aquí para evitar problemas de disposición
        }

        [Test]
        public async Task InvokeAsync_WithSuccessfulResponse_ShouldCallNext()
        {
            // Arrange
            _httpContext.Response.Body = new MemoryStream();
            _httpContext.Response.StatusCode = 200;
            _mockNext.Setup(n => n(_httpContext)).Returns(Task.CompletedTask);

            // Act
            await _middleware.InvokeAsync(_httpContext);

            // Assert
            _mockNext.Verify(n => n(_httpContext), Times.Once);
            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task InvokeAsync_WithErrorResponse_ShouldCallNext()
        {
            // Arrange
            _httpContext.Response.Body = new MemoryStream();
            _httpContext.Response.StatusCode = 500;
            _mockNext.Setup(n => n(_httpContext)).Returns(Task.CompletedTask);

            // Act
            await _middleware.InvokeAsync(_httpContext);

            // Assert
            _mockNext.Verify(n => n(_httpContext), Times.Once);
            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(500));
        }

        [Test]
        public async Task InvokeAsync_WithException_ShouldHandleException()
        {
            // Arrange
            var exception = new Exception("Test exception");
            _mockNext.Setup(n => n(_httpContext)).ThrowsAsync(exception);
            
            // Crear un nuevo stream para cada test para evitar problemas de disposición
            var testStream = new MemoryStream();
            _httpContext.Response.Body = testStream;

            // Act
            await _middleware.InvokeAsync(_httpContext);

            // Assert
            Assert.That(_httpContext.Response.StatusCode, Is.EqualTo(500));
            Assert.That(_httpContext.Response.ContentType, Is.EqualTo("application/json"));
        }

        [Test]
        public void GetErrorMessage_WithStatusCode400_ShouldReturnCorrectMessage()
        {
            // Arrange
            var middleware = new ResponseMiddleware(_mockNext.Object, _mockLogger.Object);

            // Act
            var result = middleware.GetErrorMessage(400);

            // Assert
            Assert.That(result, Is.EqualTo("Solicitud incorrecta"));
        }

        [Test]
        public void GetErrorMessage_WithStatusCode404_ShouldReturnCorrectMessage()
        {
            // Arrange
            var middleware = new ResponseMiddleware(_mockNext.Object, _mockLogger.Object);

            // Act
            var result = middleware.GetErrorMessage(404);

            // Assert
            Assert.That(result, Is.EqualTo("Recurso no encontrado"));
        }

        [Test]
        public void GetErrorMessage_WithStatusCode500_ShouldReturnCorrectMessage()
        {
            // Arrange
            var middleware = new ResponseMiddleware(_mockNext.Object, _mockLogger.Object);

            // Act
            var result = middleware.GetErrorMessage(500);

            // Assert
            Assert.That(result, Is.EqualTo("Error interno del servidor"));
        }
    }
}
