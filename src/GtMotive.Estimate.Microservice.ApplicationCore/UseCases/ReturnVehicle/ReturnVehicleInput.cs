using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Input data for returning a rented vehicle.
    /// </summary>
    public sealed class ReturnVehicleInput(LicensePlate licensePlate, PersonalIdentificationNumber customerIdNumber) : IUseCaseInput
    {
        /// <summary>
        /// Gets the license plate of the vehicle to return.
        /// </summary>
        public LicensePlate LicensePlate { get; } = licensePlate;

        /// <summary>
        /// Gets the customer's identification number for security validation.
        /// </summary>
        public PersonalIdentificationNumber CustomerIdNumber { get; } = customerIdNumber;
    }
}
