using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle
{
    /// <summary>
    /// Output data for the add vehicle use case.
    /// </summary>
    public sealed class AddVehicleOutput(Guid vehicleId, string makeAndModel, LicensePlate licensePlate, DateTime manufacturingDate) : IUseCaseOutput
    {
        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the vehicle make and model (e.g. Hyundai Tucson).
        /// </summary>
        public string MakeAndModel { get; } = makeAndModel;

        /// <summary>
        /// Gets the license plate.
        /// </summary>
        public LicensePlate LicensePlate { get; } = licensePlate;

        /// <summary>
        /// Gets the manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; } = manufacturingDate;
    }
}
