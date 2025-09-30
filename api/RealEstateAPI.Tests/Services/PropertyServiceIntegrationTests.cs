using AutoMapper;
using Moq;
using NUnit.Framework;
using RealEstateAPI.Business;
using RealEstateAPI.Business.MappingProfiles;
using RealEstateAPI.Core.DTOs;
using RealEstateAPI.Core.Entities;
using RealEstateAPI.Core.Interfaces;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class PropertyServiceIntegrationTests
    {
        private Mock<IPropertyRepository> _mockRepository;
        private IMapper _mapper;
        private PropertyService _propertyService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IPropertyRepository>();
            
            // Configurar AutoMapper con el perfil real
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PropertyProfile>();
            });
            _mapper = config.CreateMapper();
            
            _propertyService = new PropertyService(_mockRepository.Object, _mapper);
        }

        [Test]
        public async Task GetAllPropertiesAsync_ShouldReturnMappedProperties()
        {
            // Arrange
            var properties = new List<Property>
            {
                CreateSampleProperty("1", "Casa Test 1", "Dirección 1", 100000),
                CreateSampleProperty("2", "Casa Test 2", "Dirección 2", 200000)
            };
            
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(properties);

            // Act
            var result = await _propertyService.GetAllPropertiesAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            
            var firstProperty = result.First();
            Assert.That(firstProperty.Name, Is.EqualTo("Casa Test 1"));
            Assert.That(firstProperty.Price, Is.EqualTo(100000));
            Assert.That(firstProperty.Image, Is.EqualTo("https://example.com/image.jpg")); // Mapeo de imagen
        }

        [Test]
        public async Task GetPropertyByIdAsync_WithValidId_ShouldReturnMappedProperty()
        {
            // Arrange
            var propertyId = "123";
            var property = CreateSampleProperty(propertyId, "Casa Test", "Dirección Test", 150000);
            
            _mockRepository.Setup(r => r.GetByIdAsync(propertyId)).ReturnsAsync(property);

            // Act
            var result = await _propertyService.GetPropertyByIdAsync(propertyId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(propertyId));
            Assert.That(result.Name, Is.EqualTo("Casa Test"));
            Assert.That(result.Price, Is.EqualTo(150000));
            Assert.That(result.Image, Is.EqualTo("https://example.com/image.jpg"));
        }

        [Test]
        public async Task GetPropertyByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var propertyId = "999";
            _mockRepository.Setup(r => r.GetByIdAsync(propertyId)).ReturnsAsync((Property?)null);

            // Act
            var result = await _propertyService.GetPropertyByIdAsync(propertyId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task GetFilteredPropertiesAsync_WithNameFilter_ShouldReturnFilteredProperties()
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
            var result = await _propertyService.GetFilteredPropertiesAsync(filter);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task CreatePropertyAsync_ShouldCreateAndReturnMappedProperty()
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
            
            var createdProperty = CreateSampleProperty("123", propertyDto.Name, propertyDto.Address, propertyDto.Price);
            _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Property>())).ReturnsAsync(createdProperty);

            // Act
            var result = await _propertyService.CreatePropertyAsync(propertyDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(propertyDto.Name));
            Assert.That(result.Address, Is.EqualTo(propertyDto.Address));
            Assert.That(result.Price, Is.EqualTo(propertyDto.Price));
            _mockRepository.Verify(r => r.CreateAsync(It.IsAny<Property>()), Times.Once);
        }

        [Test]
        public async Task UpdatePropertyAsync_WithValidId_ShouldUpdateAndReturnMappedProperty()
        {
            // Arrange
            var propertyId = "123";
            var existingProperty = CreateSampleProperty(propertyId, "Casa Original", "Dirección Original", 100000);
            var updatedPropertyDto = new PropertyDto
            {
                Name = "Casa Actualizada",
                Address = "Dirección Actualizada",
                Price = 150000,
                IdOwner = "507f1f77bcf86cd799439011",
                CodeInternal = "PROP-001",
                Year = 2023
            };
            
            _mockRepository.Setup(r => r.GetByIdAsync(propertyId)).ReturnsAsync(existingProperty);
            _mockRepository.Setup(r => r.UpdateAsync(propertyId, It.IsAny<Property>())).ReturnsAsync(existingProperty);

            // Act
            var result = await _propertyService.UpdatePropertyAsync(propertyId, updatedPropertyDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            _mockRepository.Verify(r => r.UpdateAsync(propertyId, It.IsAny<Property>()), Times.Once);
        }

        [Test]
        public async Task UpdatePropertyAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var propertyId = "999";
            var updatedPropertyDto = new PropertyDto
            {
                Name = "Casa Actualizada",
                Address = "Dirección Actualizada",
                Price = 150000
            };
            
            _mockRepository.Setup(r => r.GetByIdAsync(propertyId)).ReturnsAsync((Property?)null);

            // Act
            var result = await _propertyService.UpdatePropertyAsync(propertyId, updatedPropertyDto);

            // Assert
            Assert.That(result, Is.Null);
            _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<string>(), It.IsAny<Property>()), Times.Never);
        }

        [Test]
        public async Task DeletePropertyAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var propertyId = "123";
            _mockRepository.Setup(r => r.DeleteAsync(propertyId)).ReturnsAsync(true);

            // Act
            var result = await _propertyService.DeletePropertyAsync(propertyId);

            // Assert
            Assert.That(result, Is.True);
            _mockRepository.Verify(r => r.DeleteAsync(propertyId), Times.Once);
        }

        [Test]
        public async Task DeletePropertyAsync_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            var propertyId = "999";
            _mockRepository.Setup(r => r.DeleteAsync(propertyId)).ReturnsAsync(false);

            // Act
            var result = await _propertyService.DeletePropertyAsync(propertyId);

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
                IdOwner = "507f1f77bcf86cd799439011",
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
