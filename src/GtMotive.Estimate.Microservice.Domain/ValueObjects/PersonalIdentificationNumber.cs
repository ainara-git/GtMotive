using System.Text.RegularExpressions;
using GtMotive.Estimate.Microservice.Domain.Exceptions;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Represents a customer's identification number (e.g., DNI) as a Value Object. Must consist of 8 digits followed by a letter (e.g., 12345678A).
    /// Equality is based on the value of the identification number.
    /// </summary>
    public sealed partial record PersonalIdentificationNumber
    {
        private static readonly Regex DniPattern = CreateDniRegex();

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonalIdentificationNumber"/> class.
        /// </summary>
        /// <param name="value">The identification number string.</param>
        /// <exception cref="DomainException">Thrown if the value is null, empty, or invalid.</exception>
        public PersonalIdentificationNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("Identification number is required.");
            }

            value = value.Trim().ToUpperInvariant();

            if (!DniPattern.IsMatch(value))
            {
                throw new DomainException($"Invalid identification number '{value}'. Must consist of 8 digits followed by a letter (e.g., 12345678A).");
            }

            Value = value;
        }

        /// <summary>
        /// Gets the normalized identification number value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Returns the string representation of the identification number.
        /// </summary>
        /// <returns>String representation of the identification number.</returns>
        public override string ToString() => Value;

        [GeneratedRegex(@"^\d{8}[A-Z]$")]
        private static partial Regex CreateDniRegex();
    }
}
