using System.Text.RegularExpressions;
using GtMotive.Estimate.Microservice.Domain.Exceptions;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Represents a vehicle's license plate as a Value Object. Must consist of 4 digits followed by 3 letters (e.g., 1234ABC).
    /// Equality is based on the value of the license plate.
    /// </summary>
    public sealed partial record LicensePlate
    {
        private static readonly Regex LicensePlatePattern = CreatePlateRegex();

        /// <summary>
        /// Initializes a new instance of the <see cref="LicensePlate"/> class.
        /// </summary>
        /// <param name="value">The license plate string.</param>
        /// <exception cref="DomainException">Thrown if the value is null, empty, or invalid.</exception>
        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("License plate is required.");
            }

            value = value.ToUpperInvariant().Trim();

            if (!LicensePlatePattern.IsMatch(value))
            {
                throw new DomainException($"Invalid license plate '{value}'. Must consist of 4 digits followed by 3 letters (e.g., 1234ABC).");
            }

            Value = value;
        }

        /// <summary>
        /// Gets the normalized license plate value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Returns the string representation of the license plate.
        /// </summary>
        /// <returns>The license plate string.</returns>
        public override string ToString() => Value;

        [GeneratedRegex(@"^\d{4}[A-Z]{3}$")]
        private static partial Regex CreatePlateRegex();
    }
}
