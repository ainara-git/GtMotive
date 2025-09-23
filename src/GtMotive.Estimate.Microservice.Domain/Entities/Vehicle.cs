using System;
using GtMotive.Estimate.Microservice.Domain.Common;
using GtMotive.Estimate.Microservice.Domain.Constants;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Vehicle entity acting as Aggregate Root for representing a vehicle in the fleet.
    /// </summary>
    public class Vehicle : EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="makeAndModel">The vehicle makeAndModel.</param>
        /// <param name="licensePlate">The vehicle license plate.</param>
        /// <param name="manufacturingDate">The manufacturing date.</param>
        public Vehicle(string makeAndModel, LicensePlate licensePlate, DateTime manufacturingDate)
        {
            Id = Guid.NewGuid();
            MakeAndModel = makeAndModel ?? throw new ArgumentNullException(nameof(makeAndModel), "makeAndModel is required.");
            LicensePlate = licensePlate ?? throw new ArgumentNullException(nameof(licensePlate), "licensePlate is required.");
            ManufacturingDate = manufacturingDate;

            // Business invariant: Vehicle must not be older than 5 years.
            if (manufacturingDate < DateTimeProvider.Today.AddYears(-DomainConstants.MaxFleetAgeYears))
            {
                throw new DomainException($"Vehicles older than {DomainConstants.MaxFleetAgeYears} years cannot be added to the fleet.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class for rehydration from storage without domain validations.
        /// </summary>
        /// <remarks>
        /// Bypasses age validation to allow reconstruction of legacy vehicles that may exceed current age limits.
        /// </remarks>
        /// <param name="id">The vehicle ID.</param>
        /// <param name="makeAndModel">The vehicle make and model.</param>
        /// <param name="licensePlate">The vehicle license plate.</param>
        /// <param name="manufacturingDate">The manufacturing date.</param>
        internal Vehicle(Guid id, string makeAndModel, LicensePlate licensePlate, DateTime manufacturingDate)
        {
            Id = id;
            MakeAndModel = makeAndModel ?? throw new ArgumentNullException(nameof(makeAndModel), "makeAndModel is required.");
            LicensePlate = licensePlate ?? throw new ArgumentNullException(nameof(licensePlate), "licensePlate is required.");
            ManufacturingDate = manufacturingDate;
        }

        /// <summary>
        /// Gets the vehicle Make and Model (e.g., "Hyundai Tucson").
        /// </summary>
        public string MakeAndModel { get; private set; }

        /// <summary>
        /// Gets the vehicle license plate.
        /// </summary>
        public LicensePlate LicensePlate { get; private set; }

        /// <summary>
        /// Gets the manufacturing date of the vehicle.
        /// </summary>
        public DateTime ManufacturingDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether checks if the vehicle is too old for fleet.
        /// </summary>
        public bool IsTooOldForFleet => ManufacturingDate < DateTimeProvider.Today.AddYears(-DomainConstants.MaxFleetAgeYears);
    }
}
