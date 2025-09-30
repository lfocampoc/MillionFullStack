using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using RealEstateAPI.API.Controllers;
using RealEstateAPI.Core.DTOs;
using RealEstateAPI.Core.Interfaces;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class PropertiesControllerIntegrationTests
    {
        private Mock<IPropertyService> _mockPropertyService;
        private Mock<ILogger<PropertiesController>> _mockLogger;
        private PropertiesController _controller;

        [SetUp]
        public void Setup()
        {
            _mockPropertyService = new Mock<IPropertyService>();
            _mockLogger = new Mock<ILogger<PropertiesController>>();
            _controller = new PropertiesController(_mockPropertyService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task GetProperties_WithValidFilter_ShouldReturnOkResult()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                Name = "Casa",
                Page = 1,
                PageSize = 10
            };
            
            var properties = new List<PropertyDto>
            {
                new PropertyDto { Id = "1", Name = "Casa Test 1", Address = "Dirección 1", Price = 100000 },
                new PropertyDto { Id = "2", Name = "Casa Test 2", Address = "Dirección 2", Price = 200000 }
            };
            
            _mockPropertyService.Setup(s => s.GetFilteredPropertiesAsync(filter)).ReturnsAsync(properties);

            // Act
            var result = await _controller.GetProperties(filter);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            
            var apiResponse = okResult.Value as ApiResponse<IEnumerable<PropertyDto>>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.True);
            Assert.That(apiResponse.Data.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetProperties_WithServiceException_ShouldReturnErrorResponse()
        {
            // Arrange
            var filter = new PropertyFilterDto();
            _mockPropertyService.Setup(s => s.GetFilteredPropertiesAsync(filter))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetProperties(filter);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            
            var apiResponse = okResult.Value as ApiResponse<IEnumerable<PropertyDto>>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.False);
            Assert.That(apiResponse.Error, Is.EqualTo("Database error"));
        }

        [Test]
        public async Task GetProperty_WithValidId_ShouldReturnOkResult()
        {
            // Arrange
            var propertyId = "123";
            var property = new PropertyDto
            {
                Id = propertyId,
                Name = "Casa Test",
                Address = "Dirección Test",
                Price = 150000
            };
            
            _mockPropertyService.Setup(s => s.GetPropertyByIdAsync(propertyId)).ReturnsAsync(property);

            // Act
            var result = await _controller.GetProperty(propertyId);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            
            var apiResponse = okResult.Value as ApiResponse<PropertyDto>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.True);
            Assert.That(apiResponse.Data.Id, Is.EqualTo(propertyId));
        }

        [Test]
        public async Task GetProperty_WithInvalidId_ShouldReturnNotFoundResponse()
        {
            // Arrange
            var propertyId = "999";
            _mockPropertyService.Setup(s => s.GetPropertyByIdAsync(propertyId)).ReturnsAsync((PropertyDto?)null);

            // Act
            var result = await _controller.GetProperty(propertyId);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            
            var apiResponse = okResult.Value as ApiResponse<PropertyDto>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.False);
            Assert.That(apiResponse.Error, Is.EqualTo("Propiedad no encontrada"));
        }

        [Test]
        public async Task CreateProperty_WithValidData_ShouldReturnCreatedProperty()
        {
            // Arrange
            var propertyDto = new PropertyDto
            {
                Name = "Nueva Casa",
                Address = "Nueva Dirección",
                Price = 300000,
                IdOwner = "507f1f77bcf86cd799439011",
                CodeInternal = "PROP-001",
                Year = 2023
            };
            
            var createdProperty = new PropertyDto
            {
                Id = "123",
                Name = propertyDto.Name,
                Address = propertyDto.Address,
                Price = propertyDto.Price,
                IdOwner = propertyDto.IdOwner,
                CodeInternal = propertyDto.CodeInternal,
                Year = propertyDto.Year
            };
            
            _mockPropertyService.Setup(s => s.CreatePropertyAsync(propertyDto)).ReturnsAsync(createdProperty);

            // Act
            var result = await _controller.CreateProperty(propertyDto);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            
            var apiResponse = okResult.Value as ApiResponse<PropertyDto>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.True);
            Assert.That(apiResponse.Data.Name, Is.EqualTo(propertyDto.Name));
        }

        [Test]
        public async Task UpdateProperty_WithValidId_ShouldReturnUpdatedProperty()
        {
            // Arrange
            var propertyId = "123";
            var propertyDto = new PropertyDto
            {
                Name = "Casa Actualizada",
                Address = "Dirección Actualizada",
                Price = 200000,
                IdOwner = "507f1f77bcf86cd799439011",
                CodeInternal = "PROP-001",
                Year = 2023
            };
            
            var updatedProperty = new PropertyDto
            {
                Id = propertyId,
                Name = propertyDto.Name,
                Address = propertyDto.Address,
                Price = propertyDto.Price,
                IdOwner = propertyDto.IdOwner,
                CodeInternal = propertyDto.CodeInternal,
                Year = propertyDto.Year
            };
            
            _mockPropertyService.Setup(s => s.UpdatePropertyAsync(propertyId, propertyDto)).ReturnsAsync(updatedProperty);

            // Act
            var result = await _controller.UpdateProperty(propertyId, propertyDto);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            
            var apiResponse = okResult.Value as ApiResponse<PropertyDto>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.True);
            Assert.That(apiResponse.Data.Name, Is.EqualTo(propertyDto.Name));
        }

        [Test]
        public async Task UpdateProperty_WithInvalidId_ShouldReturnNotFoundResponse()
        {
            // Arrange
            var propertyId = "999";
            var propertyDto = new PropertyDto
            {
                Name = "Casa Actualizada",
                Address = "Dirección Actualizada",
                Price = 200000
            };
            
            _mockPropertyService.Setup(s => s.UpdatePropertyAsync(propertyId, propertyDto)).ReturnsAsync((PropertyDto?)null);

            // Act
            var result = await _controller.UpdateProperty(propertyId, propertyDto);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            
            var apiResponse = okResult.Value as ApiResponse<PropertyDto>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.False);
            Assert.That(apiResponse.Error, Is.EqualTo("Propiedad no encontrada"));
        }

        [Test]
        public async Task DeleteProperty_WithValidId_ShouldReturnSuccessResponse()
        {
            // Arrange
            var propertyId = "123";
            _mockPropertyService.Setup(s => s.DeletePropertyAsync(propertyId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteProperty(propertyId);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            
            var apiResponse = okResult.Value as ApiResponse<bool>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.True);
            Assert.That(apiResponse.Data, Is.True);
        }

        [Test]
        public async Task DeleteProperty_WithInvalidId_ShouldReturnNotFoundResponse()
        {
            // Arrange
            var propertyId = "999";
            _mockPropertyService.Setup(s => s.DeletePropertyAsync(propertyId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteProperty(propertyId);

            // Assert
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            
            var apiResponse = okResult.Value as ApiResponse<bool>;
            Assert.That(apiResponse, Is.Not.Null);
            Assert.That(apiResponse.Success, Is.False);
            Assert.That(apiResponse.Error, Is.EqualTo("Propiedad no encontrada"));
        }
    }
}
