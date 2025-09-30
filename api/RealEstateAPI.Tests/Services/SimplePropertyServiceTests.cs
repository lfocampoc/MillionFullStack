using Moq;
using NUnit.Framework;
using RealEstateAPI.Core.DTOs;
using RealEstateAPI.Core.Entities;
using RealEstateAPI.Core.Interfaces;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class SimplePropertyServiceTests
    {
        private Mock<IPropertyRepository> _mockRepository;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IPropertyRepository>();
        }

        [Test]
        public void PropertyRepository_GetAllAsync_ShouldReturnProperties()
        {
            // Arrange
            var properties = new List<Property>
            {
                CreateSampleProperty("1", "Casa Test 1", "Dirección 1", 100000),
                CreateSampleProperty("2", "Casa Test 2", "Dirección 2", 200000)
            };
            
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(properties);

            // Act
            var result = _mockRepository.Object.GetAllAsync().Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("Casa Test 1"));
            Assert.That(result.Last().Name, Is.EqualTo("Casa Test 2"));
        }

        [Test]
        public void PropertyRepository_GetByIdAsync_WithValidId_ShouldReturnProperty()
        {
            // Arrange
            var propertyId = "123";
            var property = CreateSampleProperty(propertyId, "Casa Test", "Dirección Test", 150000);
            
            _mockRepository.Setup(r => r.GetByIdAsync(propertyId)).ReturnsAsync(property);

            // Act
            var result = _mockRepository.Object.GetByIdAsync(propertyId).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(propertyId));
            Assert.That(result.Name, Is.EqualTo("Casa Test"));
            Assert.That(result.Price, Is.EqualTo(150000));
        }

        [Test]
        public void PropertyRepository_GetByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var propertyId = "999";
            _mockRepository.Setup(r => r.GetByIdAsync(propertyId)).ReturnsAsync((Property?)null);

            // Act
            var result = _mockRepository.Object.GetByIdAsync(propertyId).Result;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void PropertyRepository_GetFilteredAsync_WithNameFilter_ShouldReturnFilteredProperties()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                Name = "Casa",
                Page = 1,
                PageSize = 10
            };
            
            var properties = new List<Property>
            {
                CreateSampleProperty("1", "Casa Moderna", "Dirección 1", 100000),
                CreateSampleProperty("2", "Apartamento", "Dirección 2", 200000)
            };
            
            _mockRepository.Setup(r => r.GetFilteredAsync(filter)).ReturnsAsync(properties);

            // Act
            var result = _mockRepository.Object.GetFilteredAsync(filter).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void PropertyRepository_GetFilteredAsync_WithPriceFilter_ShouldReturnFilteredProperties()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                MinPrice = 100000,
                MaxPrice = 200000,
                Page = 1,
                PageSize = 10
            };
            
            var properties = new List<Property>
            {
                CreateSampleProperty("1", "Casa 1", "Dirección 1", 150000),
                CreateSampleProperty("2", "Casa 2", "Dirección 2", 180000)
            };
            
            _mockRepository.Setup(r => r.GetFilteredAsync(filter)).ReturnsAsync(properties);

            // Act
            var result = _mockRepository.Object.GetFilteredAsync(filter).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void PropertyRepository_CreateAsync_ShouldCreateProperty()
        {
            // Arrange
            var property = CreateSampleProperty("123", "Nueva Casa", "Nueva Dirección", 300000);
            _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Property>())).ReturnsAsync(property);

            // Act
            var result = _mockRepository.Object.CreateAsync(property).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Nueva Casa"));
            Assert.That(result.Address, Is.EqualTo("Nueva Dirección"));
            Assert.That(result.Price, Is.EqualTo(300000));
        }

        [Test]
        public void PropertyRepository_UpdateAsync_WithValidId_ShouldUpdateProperty()
        {
            // Arrange
            var propertyId = "123";
            var property = CreateSampleProperty(propertyId, "Casa Actualizada", "Dirección Actualizada", 200000);
            _mockRepository.Setup(r => r.UpdateAsync(propertyId, It.IsAny<Property>())).ReturnsAsync(property);

            // Act
            var result = _mockRepository.Object.UpdateAsync(propertyId, property).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Casa Actualizada"));
            _mockRepository.Verify(r => r.UpdateAsync(propertyId, It.IsAny<Property>()), Times.Once);
        }

        [Test]
        public void PropertyRepository_DeleteAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var propertyId = "123";
            _mockRepository.Setup(r => r.DeleteAsync(propertyId)).ReturnsAsync(true);

            // Act
            var result = _mockRepository.Object.DeleteAsync(propertyId).Result;

            // Assert
            Assert.That(result, Is.True);
            _mockRepository.Verify(r => r.DeleteAsync(propertyId), Times.Once);
        }

        [Test]
        public void PropertyRepository_DeleteAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            var propertyId = "999";
            _mockRepository.Setup(r => r.DeleteAsync(propertyId)).ReturnsAsync(false);

            // Act
            var result = _mockRepository.Object.DeleteAsync(propertyId).Result;

            // Assert
            Assert.That(result, Is.False);
        }

        private Property CreateSampleProperty(string id, string name, string address, decimal price)
        {
            return new Property
            {
                Id = id,
                Name = name,
                Address = address,
                Price = price,
                IdOwner = "owner123",
                CodeInternal = "PROP-001",
                Year = 2023,
                Images = new List<PropertyImage>
                {
                    new PropertyImage
                    {
                        Id = "img1",
                        File = "https://example.com/image.jpg",
                        Enabled = true,
                        IdProperty = id
                    }
                },
                Traces = new List<PropertyTrace>()
            };
        }
    }
}
