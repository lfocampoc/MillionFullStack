using FluentValidation.TestHelper;
using NUnit.Framework;
using RealEstateAPI.Core.DTOs;
using RealEstateAPI.Core.Validators;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class PropertyFilterDtoValidatorTests
    {
        private PropertyFilterDtoValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new PropertyFilterDtoValidator();
        }

        [Test]
        public void Validate_WithValidFilter_ShouldNotHaveValidationError()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                Name = "Casa",
                Address = "Centro",
                MinPrice = 100000,
                MaxPrice = 500000,
                Page = 1,
                PageSize = 10
            };

            // Act
            var result = _validator.TestValidate(filter);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_WithEmptyFilter_ShouldNotHaveValidationError()
        {
            // Arrange
            var filter = new PropertyFilterDto();

            // Act
            var result = _validator.TestValidate(filter);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_WithNegativeMinPrice_ShouldHaveValidationError()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                MinPrice = -1000
            };

            // Act
            var result = _validator.TestValidate(filter);

            // Assert
            result.ShouldHaveValidationErrorFor(f => f.MinPrice);
        }

        [Test]
        public void Validate_WithNegativeMaxPrice_ShouldHaveValidationError()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                MinPrice = 100000,
                MaxPrice = -1000
            };

            // Act
            var result = _validator.TestValidate(filter);

            // Assert
            result.ShouldHaveValidationErrorFor(f => f.MaxPrice);
        }

        [Test]
        public void Validate_WithMinPriceGreaterThanMaxPrice_ShouldHaveValidationError()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                MinPrice = 500000,
                MaxPrice = 100000
            };

            // Act
            var result = _validator.TestValidate(filter);

            // Assert
            result.ShouldHaveValidationErrorFor(f => f.MaxPrice);
        }

        [Test]
        public void Validate_WithZeroPage_ShouldHaveValidationError()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                Page = 0
            };

            // Act
            var result = _validator.TestValidate(filter);

            // Assert
            result.ShouldHaveValidationErrorFor(f => f.Page);
        }

        [Test]
        public void Validate_WithNegativePage_ShouldHaveValidationError()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                Page = -1
            };

            // Act
            var result = _validator.TestValidate(filter);

            // Assert
            result.ShouldHaveValidationErrorFor(f => f.Page);
        }

        [Test]
        public void Validate_WithZeroPageSize_ShouldHaveValidationError()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                PageSize = 0
            };

            // Act
            var result = _validator.TestValidate(filter);

            // Assert
            result.ShouldHaveValidationErrorFor(f => f.PageSize);
        }

        [Test]
        public void Validate_WithNegativePageSize_ShouldHaveValidationError()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                PageSize = -1
            };

            // Act
            var result = _validator.TestValidate(filter);

            // Assert
            result.ShouldHaveValidationErrorFor(f => f.PageSize);
        }

        [Test]
        public void Validate_WithPageSizeTooLarge_ShouldHaveValidationError()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                PageSize = 101 // Más de 100
            };

            // Act
            var result = _validator.TestValidate(filter);

            // Assert
            result.ShouldHaveValidationErrorFor(f => f.PageSize);
        }

        [Test]
        public void Validate_WithValidPriceRange_ShouldNotHaveValidationError()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                MinPrice = 100000,
                MaxPrice = 500000
            };

            // Act
            var result = _validator.TestValidate(filter);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_WithSameMinAndMaxPrice_ShouldNotHaveValidationError()
        {
            // Arrange
            var filter = new PropertyFilterDto
            {
                MinPrice = 250000,
                MaxPrice = 250000
            };

            // Act
            var result = _validator.TestValidate(filter);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
