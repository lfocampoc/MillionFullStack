using FluentValidation.TestHelper;
using NUnit.Framework;
using RealEstateAPI.Core.DTOs;
using RealEstateAPI.Core.Validators;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class PropertyDtoValidatorTests
    {
        private PropertyDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new PropertyDtoValidator();
        }

        [Test]
        public void Validate_WithValidProperty_ShouldNotHaveValidationError()
        {
            // Arrange
            var property = new PropertyDto
            {
                Name = "Casa Test",
                Address = "Dirección Test 123",
                Price = 150000,
                IdOwner = "507f1f77bcf86cd799439011", // ObjectId válido
                CodeInternal = "PROP-001",
                Year = 2023
            };

            // Act
            var result = _validator.TestValidate(property);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_WithEmptyName_ShouldHaveValidationError()
        {
            // Arrange
            var property = new PropertyDto
            {
                Name = "",
                Address = "Dirección Test",
                Price = 150000,
                IdOwner = "owner123",
                CodeInternal = "PROP-001",
                Year = 2023
            };

            // Act
            var result = _validator.TestValidate(property);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Name);
        }

        [Test]
        public void Validate_WithNullName_ShouldHaveValidationError()
        {
            // Arrange
            var property = new PropertyDto
            {
                Name = null!,
                Address = "Dirección Test",
                Price = 150000,
                IdOwner = "owner123",
                CodeInternal = "PROP-001",
                Year = 2023
            };

            // Act
            var result = _validator.TestValidate(property);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Name);
        }

        [Test]
        public void Validate_WithNameTooLong_ShouldHaveValidationError()
        {
            // Arrange
            var property = new PropertyDto
            {
                Name = new string('A', 201), // Más de 200 caracteres
                Address = "Dirección Test",
                Price = 150000,
                IdOwner = "owner123",
                CodeInternal = "PROP-001",
                Year = 2023
            };

            // Act
            var result = _validator.TestValidate(property);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Name);
        }

        [Test]
        public void Validate_WithEmptyAddress_ShouldHaveValidationError()
        {
            // Arrange
            var property = new PropertyDto
            {
                Name = "Casa Test",
                Address = "",
                Price = 150000,
                IdOwner = "owner123",
                CodeInternal = "PROP-001",
                Year = 2023
            };

            // Act
            var result = _validator.TestValidate(property);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Address);
        }

        [Test]
        public void Validate_WithNegativePrice_ShouldHaveValidationError()
        {
            // Arrange
            var property = new PropertyDto
            {
                Name = "Casa Test",
                Address = "Dirección Test",
                Price = -1000,
                IdOwner = "owner123",
                CodeInternal = "PROP-001",
                Year = 2023
            };

            // Act
            var result = _validator.TestValidate(property);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Price);
        }

        [Test]
        public void Validate_WithZeroPrice_ShouldHaveValidationError()
        {
            // Arrange
            var property = new PropertyDto
            {
                Name = "Casa Test",
                Address = "Dirección Test",
                Price = 0,
                IdOwner = "owner123",
                CodeInternal = "PROP-001",
                Year = 2023
            };

            // Act
            var result = _validator.TestValidate(property);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Price);
        }

        [Test]
        public void Validate_WithEmptyIdOwner_ShouldHaveValidationError()
        {
            // Arrange
            var property = new PropertyDto
            {
                Name = "Casa Test",
                Address = "Dirección Test",
                Price = 150000,
                IdOwner = "",
                CodeInternal = "PROP-001",
                Year = 2023
            };

            // Act
            var result = _validator.TestValidate(property);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.IdOwner);
        }

        [Test]
        public void Validate_WithEmptyCodeInternal_ShouldHaveValidationError()
        {
            // Arrange
            var property = new PropertyDto
            {
                Name = "Casa Test",
                Address = "Dirección Test",
                Price = 150000,
                IdOwner = "owner123",
                CodeInternal = "",
                Year = 2023
            };

            // Act
            var result = _validator.TestValidate(property);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.CodeInternal);
        }

        [Test]
        public void Validate_WithInvalidYear_ShouldHaveValidationError()
        {
            // Arrange
            var property = new PropertyDto
            {
                Name = "Casa Test",
                Address = "Dirección Test",
                Price = 150000,
                IdOwner = "owner123",
                CodeInternal = "PROP-001",
                Year = 1800 // Año muy antiguo
            };

            // Act
            var result = _validator.TestValidate(property);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Year);
        }

        [Test]
        public void Validate_WithFutureYear_ShouldHaveValidationError()
        {
            // Arrange
            var property = new PropertyDto
            {
                Name = "Casa Test",
                Address = "Dirección Test",
                Price = 150000,
                IdOwner = "507f1f77bcf86cd799439011", // ObjectId válido
                CodeInternal = "PROP-001",
                Year = DateTime.Now.Year + 2 // Año futuro
            };

            // Act
            var result = _validator.TestValidate(property);

            // Assert
            result.ShouldHaveValidationErrorFor(p => p.Year);
        }
    }
}
