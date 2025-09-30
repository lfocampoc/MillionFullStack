using NUnit.Framework;
using RealEstateAPI.API.Data;
using RealEstateAPI.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAPI.Tests
{
    [TestFixture]
    public class SeedDataTests
    {

        [Test]
        public void CreateSampleOwners_ShouldReturnValidOwners()
        {
            // Act
            var owners = SeedData.CreateSampleOwners();

            // Assert
            Assert.That(owners, Is.Not.Null);
            Assert.That(owners.Count, Is.GreaterThan(0));
            Assert.That(owners.All(o => !string.IsNullOrEmpty(o.Name)), Is.True);
            Assert.That(owners.All(o => !string.IsNullOrEmpty(o.Address)), Is.True);
        }

        [Test]
        public void CreateSampleProperties_ShouldReturnValidProperties()
        {
            // Arrange
            var owners = SeedData.CreateSampleOwners();

            // Act
            var properties = SeedData.CreateSampleProperties(owners);

            // Assert
            Assert.That(properties, Is.Not.Null);
            Assert.That(properties.Count, Is.GreaterThan(0));
            Assert.That(properties.All(p => !string.IsNullOrEmpty(p.Name)), Is.True);
            Assert.That(properties.All(p => !string.IsNullOrEmpty(p.Address)), Is.True);
            Assert.That(properties.All(p => p.Price > 0), Is.True);
            Assert.That(properties.All(p => !string.IsNullOrEmpty(p.IdOwner)), Is.True);
        }

        [Test]
        public void CreateSampleProperties_WithMultipleOwners_ShouldReturnValidProperties()
        {
            // Arrange
            var owners = new List<Owner>
            {
                new Owner
                {
                    Id = "test-owner-1",
                    Name = "Test Owner 1",
                    Address = "Test Address 1",
                    Photo = "test-photo-1.jpg",
                    Birthday = new DateTime(1990, 1, 1)
                },
                new Owner
                {
                    Id = "test-owner-2",
                    Name = "Test Owner 2",
                    Address = "Test Address 2",
                    Photo = "test-photo-2.jpg",
                    Birthday = new DateTime(1985, 5, 15)
                },
                new Owner
                {
                    Id = "test-owner-3",
                    Name = "Test Owner 3",
                    Address = "Test Address 3",
                    Photo = "test-photo-3.jpg",
                    Birthday = new DateTime(1992, 10, 20)
                }
            };

            // Act
            var properties = SeedData.CreateSampleProperties(owners);

            // Assert
            Assert.That(properties, Is.Not.Null);
            Assert.That(properties.Count, Is.GreaterThan(0));
            Assert.That(properties.All(p => owners.Any(o => o.Id == p.IdOwner)), Is.True);
        }

        [Test]
        public void CreateSampleProperties_ShouldHaveValidOwnerReferences()
        {
            // Arrange
            var owners = SeedData.CreateSampleOwners();
            var ownerIds = owners.Select(o => o.Id).ToList();

            // Act
            var properties = SeedData.CreateSampleProperties(owners);

            // Assert
            Assert.That(properties.All(p => ownerIds.Contains(p.IdOwner)), Is.True);
        }

        [Test]
        public void CreateSampleProperties_ShouldHaveValidImages()
        {
            // Arrange
            var owners = SeedData.CreateSampleOwners();

            // Act
            var properties = SeedData.CreateSampleProperties(owners);

            // Assert
            Assert.That(properties.All(p => p.Images != null), Is.True);
            Assert.That(properties.All(p => p.Images.Count > 0), Is.True);
            Assert.That(properties.All(p => p.Images.Any(img => img.Enabled)), Is.True);
        }

        [Test]
        public void CreateSampleProperties_ShouldHaveValidTraces()
        {
            // Arrange
            var owners = SeedData.CreateSampleOwners();

            // Act
            var properties = SeedData.CreateSampleProperties(owners);

            // Assert
            Assert.That(properties.All(p => p.Traces != null), Is.True);
            Assert.That(properties.Any(p => p.Traces.Count > 0), Is.True); // Al menos una propiedad tiene traces
            Assert.That(properties.Where(p => p.Traces.Count > 0).All(p => p.Traces.All(t => t.Value > 0)), Is.True);
        }
    }
}
