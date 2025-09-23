using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output data for the rent vehicle use case.
    /// </summary>
    public sealed class RentVehicleOutput(
        Guid rentalId,
        Guid vehicleId,
        string vehicleMakeAndModel,
        PersonalIdentificationNumber customerIdNumber,
        Period period) : IUseCaseOutput
    {
        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        public Guid RentalId { get; } = rentalId;

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the vehicle make and model.
        /// </summary>
        public string VehicleMakeAndModel { get; } = vehicleMakeAndModel;

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
