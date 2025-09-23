using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Input data for renting a vehicle by license plate.
    /// </summary>
    public sealed class RentVehicleInput(LicensePlate licensePlate, PersonalIdentificationNumber customerIdNumber, Period period) : IUseCaseInput
    {
        /// <summary>
        /// Gets the license plate of the vehicle to rent.
        /// </summary>
        public LicensePlate LicensePlate { get; } = licensePlate;

        /// <summary>
        /// Gets the customer's identification number.
        /// </summary>
        public PersonalIdentificationNumber CustomerIdNumber { get; } = customerIdNumber;

        /// <summary>
        /// Gets the rental period.
        /// </summary>
        public Period Period { get; } = period;
    }
}
