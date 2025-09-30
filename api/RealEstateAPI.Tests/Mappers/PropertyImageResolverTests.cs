using AutoMapper;
using NUnit.Framework;
using RealEstateAPI.Business.MappingProfiles;
using RealEstateAPI.Core.Entities;
using RealEstateAPI.Core.DTOs;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class PropertyImageResolverTests
    {
        private PropertyImageResolver _resolver;

        [SetUp]
        public void Setup()
        {
            _resolver = new PropertyImageResolver();
        }

        [Test]
        public void Resolve_WithEnabledImages_ShouldReturnFirstEnabledImage()
        {
            // Arrange
            var property = new Property
            {
                Images = new List<PropertyImage>
                {
                    new PropertyImage { File = "image1.jpg", Enabled = false },
                    new PropertyImage { File = "image2.jpg", Enabled = true },
                    new PropertyImage { File = "image3.jpg", Enabled = true }
                }
            };

            // Act
            var result = _resolver.Resolve(property, new PropertyDto(), null!, null);

            // Assert
            Assert.That(result, Is.EqualTo("image2.jpg"));
        }

        [Test]
        public void Resolve_WithNoEnabledImages_ShouldReturnNull()
        {
            // Arrange
            var property = new Property
            {
                Images = new List<PropertyImage>
                {
                    new PropertyImage { File = "image1.jpg", Enabled = false },
                    new PropertyImage { File = "image2.jpg", Enabled = false }
                }
            };

            // Act
            var result = _resolver.Resolve(property, new PropertyDto(), null!, null);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Resolve_WithNullImages_ShouldReturnNull()
        {
            // Arrange
            var property = new Property
            {
                Images = null
            };

            // Act
            var result = _resolver.Resolve(property, new PropertyDto(), null!, null);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Resolve_WithEmptyImages_ShouldReturnNull()
        {
            // Arrange
            var property = new Property
            {
                Images = new List<PropertyImage>()
            };

            // Act
            var result = _resolver.Resolve(property, new PropertyDto(), null!, null);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void Resolve_WithOnlyDisabledImages_ShouldReturnNull()
        {
            // Arrange
            var property = new Property
            {
                Images = new List<PropertyImage>
                {
                    new PropertyImage { File = "image1.jpg", Enabled = false }
                }
            };

            // Act
            var result = _resolver.Resolve(property, new PropertyDto(), null!, null);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
