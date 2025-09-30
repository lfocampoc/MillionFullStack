using AutoMapper;
using NUnit.Framework;
using RealEstateAPI.Business.MappingProfiles;
using RealEstateAPI.Core.Entities;
using RealEstateAPI.Core.DTOs;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class PropertyProfileTests
    {
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PropertyProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public void PropertyToPropertyDto_ShouldMapCorrectly()
        {
            // Arrange
            var property = new Property
            {
                Id = "123",
                Name = "Test Property",
                Address = "Test Address",
                Price = 100000,
                CodeInternal = "PROP-001",
                Year = 2023,
                IdOwner = "owner123",
                Images = new List<PropertyImage>
                {
                    new PropertyImage { File = "image1.jpg", Enabled = true },
                    new PropertyImage { File = "image2.jpg", Enabled = false }
                },
                Traces = new List<PropertyTrace>()
            };

            // Act
            var result = _mapper.Map<PropertyDto>(property);

            // Assert
            Assert.That(result.Id, Is.EqualTo(property.Id));
            Assert.That(result.Name, Is.EqualTo(property.Name));
            Assert.That(result.Address, Is.EqualTo(property.Address));
            Assert.That(result.Price, Is.EqualTo(property.Price));
            Assert.That(result.CodeInternal, Is.EqualTo(property.CodeInternal));
            Assert.That(result.Year, Is.EqualTo(property.Year));
            Assert.That(result.IdOwner, Is.EqualTo(property.IdOwner));
            Assert.That(result.Image, Is.EqualTo("image1.jpg")); // First enabled image
        }

        [Test]
        public void PropertyToPropertyDto_WithNoEnabledImages_ShouldMapWithNullImage()
        {
            // Arrange
            var property = new Property
            {
                Id = "123",
                Name = "Test Property",
                Address = "Test Address",
                Price = 100000,
                CodeInternal = "PROP-001",
                Year = 2023,
                IdOwner = "owner123",
                Images = new List<PropertyImage>
                {
                    new PropertyImage { File = "image1.jpg", Enabled = false }
                },
                Traces = new List<PropertyTrace>()
            };

            // Act
            var result = _mapper.Map<PropertyDto>(property);

            // Assert
            Assert.That(result.Id, Is.EqualTo(property.Id));
            Assert.That(result.Image, Is.Null);
        }

        [Test]
        public void PropertyDtoToProperty_ShouldMapCorrectly()
        {
            // Arrange
            var propertyDto = new PropertyDto
            {
                Id = "123",
                Name = "Test Property",
                Address = "Test Address",
                Price = 100000,
                CodeInternal = "PROP-001",
                Year = 2023,
                IdOwner = "owner123"
            };

            // Act
            var result = _mapper.Map<Property>(propertyDto);

            // Assert
            Assert.That(result.Id, Is.EqualTo(propertyDto.Id));
            Assert.That(result.Name, Is.EqualTo(propertyDto.Name));
            Assert.That(result.Address, Is.EqualTo(propertyDto.Address));
            Assert.That(result.Price, Is.EqualTo(propertyDto.Price));
            Assert.That(result.CodeInternal, Is.EqualTo(propertyDto.CodeInternal));
            Assert.That(result.Year, Is.EqualTo(propertyDto.Year));
            Assert.That(result.IdOwner, Is.EqualTo(propertyDto.IdOwner));
            Assert.That(result.Images, Is.Not.Null);
            Assert.That(result.Traces, Is.Not.Null);
        }

        [Test]
        public void OwnerToOwnerDto_ShouldMapCorrectly()
        {
            // Arrange
            var owner = new Owner
            {
                Id = "owner123",
                Name = "John Doe",
                Address = "123 Main St",
                Photo = "https://example.com/photo.jpg",
                Birthday = new DateTime(1990, 1, 1)
            };

            // Act
            var result = _mapper.Map<OwnerDto>(owner);

            // Assert
            Assert.That(result.Id, Is.EqualTo(owner.Id));
            Assert.That(result.Name, Is.EqualTo(owner.Name));
            Assert.That(result.Address, Is.EqualTo(owner.Address));
            Assert.That(result.Photo, Is.EqualTo(owner.Photo));
            Assert.That(result.Birthday, Is.EqualTo(owner.Birthday));
        }

        [Test]
        public void OwnerDtoToOwner_ShouldMapCorrectly()
        {
            // Arrange
            var ownerDto = new OwnerDto
            {
                Id = "owner123",
                Name = "John Doe",
                Address = "123 Main St",
                Photo = "https://example.com/photo.jpg",
                Birthday = new DateTime(1990, 1, 1)
            };

            // Act
            var result = _mapper.Map<Owner>(ownerDto);

            // Assert
            Assert.That(result.Id, Is.EqualTo(ownerDto.Id));
            Assert.That(result.Name, Is.EqualTo(ownerDto.Name));
            Assert.That(result.Address, Is.EqualTo(ownerDto.Address));
            Assert.That(result.Photo, Is.EqualTo(ownerDto.Photo));
            Assert.That(result.Birthday, Is.EqualTo(ownerDto.Birthday));
        }
    }
}
