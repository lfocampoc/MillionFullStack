using NUnit.Framework;
using RealEstateAPI.Core.Entities;
using RealEstateAPI.Core.DTOs;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void Property_ShouldHaveCorrectProperties()
        {
            // Arrange & Act
            var property = new Property
            {
                Id = "123",
                Name = "Test Property",
                Address = "Test Address",
                Price = 100000,
                CodeInternal = "PROP-001",
                Year = 2023,
                IdOwner = "owner123",
                Images = new List<PropertyImage>(),
                Traces = new List<PropertyTrace>()
            };

            // Assert
            Assert.That(property.Id, Is.EqualTo("123"));
            Assert.That(property.Name, Is.EqualTo("Test Property"));
            Assert.That(property.Address, Is.EqualTo("Test Address"));
            Assert.That(property.Price, Is.EqualTo(100000));
            Assert.That(property.CodeInternal, Is.EqualTo("PROP-001"));
            Assert.That(property.Year, Is.EqualTo(2023));
            Assert.That(property.IdOwner, Is.EqualTo("owner123"));
            Assert.That(property.Images, Is.Not.Null);
            Assert.That(property.Traces, Is.Not.Null);
        }

        [Test]
        public void PropertyImage_ShouldHaveCorrectProperties()
        {
            // Arrange & Act
            var image = new PropertyImage
            {
                Id = "img123",
                File = "https://example.com/image.jpg",
                Enabled = true,
                IdProperty = "prop123"
            };

            // Assert
            Assert.That(image.Id, Is.EqualTo("img123"));
            Assert.That(image.File, Is.EqualTo("https://example.com/image.jpg"));
            Assert.That(image.Enabled, Is.True);
            Assert.That(image.IdProperty, Is.EqualTo("prop123"));
        }

        [Test]
        public void PropertyTrace_ShouldHaveCorrectProperties()
        {
            // Arrange & Act
            var trace = new PropertyTrace
            {
                Id = "trace123",
                DateSale = new DateTime(2023, 1, 1),
                Name = "Initial Sale",
                Value = 100000,
                Tax = 10000,
                IdProperty = "prop123"
            };

            // Assert
            Assert.That(trace.Id, Is.EqualTo("trace123"));
            Assert.That(trace.DateSale, Is.EqualTo(new DateTime(2023, 1, 1)));
            Assert.That(trace.Name, Is.EqualTo("Initial Sale"));
            Assert.That(trace.Value, Is.EqualTo(100000));
            Assert.That(trace.Tax, Is.EqualTo(10000));
            Assert.That(trace.IdProperty, Is.EqualTo("prop123"));
        }

        [Test]
        public void Owner_ShouldHaveCorrectProperties()
        {
            // Arrange & Act
            var owner = new Owner
            {
                Id = "owner123",
                Name = "John Doe",
                Address = "123 Main St",
                Photo = "https://example.com/photo.jpg",
                Birthday = new DateTime(1990, 1, 1)
            };

            // Assert
            Assert.That(owner.Id, Is.EqualTo("owner123"));
            Assert.That(owner.Name, Is.EqualTo("John Doe"));
            Assert.That(owner.Address, Is.EqualTo("123 Main St"));
            Assert.That(owner.Photo, Is.EqualTo("https://example.com/photo.jpg"));
            Assert.That(owner.Birthday, Is.EqualTo(new DateTime(1990, 1, 1)));
        }

        [Test]
        public void OwnerDto_ShouldHaveCorrectProperties()
        {
            // Arrange & Act
            var ownerDto = new OwnerDto
            {
                Id = "owner123",
                Name = "John Doe",
                Address = "123 Main St",
                Photo = "https://example.com/photo.jpg",
                Birthday = new DateTime(1990, 1, 1)
            };

            // Assert
            Assert.That(ownerDto.Id, Is.EqualTo("owner123"));
            Assert.That(ownerDto.Name, Is.EqualTo("John Doe"));
            Assert.That(ownerDto.Address, Is.EqualTo("123 Main St"));
            Assert.That(ownerDto.Photo, Is.EqualTo("https://example.com/photo.jpg"));
            Assert.That(ownerDto.Birthday, Is.EqualTo(new DateTime(1990, 1, 1)));
        }

        [Test]
        public void ApiResponse_ShouldHaveCorrectProperties()
        {
            // Arrange & Act
            var apiResponse = new ApiResponse<string>
            {
                Success = true,
                Data = "Test Data",
                Message = "Success",
                Error = null
            };

            // Assert
            Assert.That(apiResponse.Success, Is.True);
            Assert.That(apiResponse.Data, Is.EqualTo("Test Data"));
            Assert.That(apiResponse.Message, Is.EqualTo("Success"));
            Assert.That(apiResponse.Error, Is.Null);
        }

        [Test]
        public void ApiResponse_ErrorResult_ShouldSetCorrectProperties()
        {
            // Arrange & Act
            var apiResponse = ApiResponse<string>.ErrorResult("Error message", "Error details");

            // Assert
            Assert.That(apiResponse.Success, Is.False);
            Assert.That(apiResponse.Data, Is.Null);
            Assert.That(apiResponse.Message, Is.EqualTo("Error details"));
            Assert.That(apiResponse.Error, Is.EqualTo("Error message"));
        }

        [Test]
        public void ApiResponse_SuccessResult_ShouldSetCorrectProperties()
        {
            // Arrange & Act
            var apiResponse = ApiResponse<string>.SuccessResult("Test Data", "Success message");

            // Assert
            Assert.That(apiResponse.Success, Is.True);
            Assert.That(apiResponse.Data, Is.EqualTo("Test Data"));
            Assert.That(apiResponse.Message, Is.EqualTo("Success message"));
            Assert.That(apiResponse.Error, Is.EqualTo(string.Empty));
        }
    }
}
