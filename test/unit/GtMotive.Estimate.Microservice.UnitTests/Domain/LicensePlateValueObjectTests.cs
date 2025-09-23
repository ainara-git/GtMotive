using System;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain
{
    public class LicensePlateValueObjectTests
    {
        [Theory]
        [InlineData("1234ABC")]
        [InlineData("0000XYZ")]
        [InlineData("9999DEF")]
        public void ConstructorWithValidLicensePlateShouldCreateSuccessfully(string validLicensePlate)
        {
            ArgumentNullException.ThrowIfNull(validLicensePlate);

            // Arrange & Act
            var licensePlate = new LicensePlate(validLicensePlate);

            // Assert
            Assert.NotNull(licensePlate);
            Assert.Equal(validLicensePlate.ToUpperInvariant(), licensePlate.Value);
        }

        [Theory]
        [InlineData("123ABC")] // Too few digits
        [InlineData("1234AB")] // Too few letters
        [InlineData("ABC1234")] // Wrong order
        [InlineData("1234ABCD")] // Too many letters
        [InlineData("12345ABC")] // Too many digits
        [InlineData("1234-ABC")] // Invalid characters
        public void ConstructorWithInvalidLicensePlateShouldThrowDomainException(string invalidLicensePlate)
        {
            ArgumentNullException.ThrowIfNull(invalidLicensePlate);

            // Arrange & Act & Assert
            var exception = Assert.Throws<DomainException>(() => new LicensePlate(invalidLicensePlate));
            Assert.Contains("Invalid license plate", exception.Message, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
