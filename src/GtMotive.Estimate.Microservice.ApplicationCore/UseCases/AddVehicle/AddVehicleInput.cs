using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle
{
    /// <summary>
    /// Input data for adding a new vehicle to the fleet.
    /// </summary>
    public sealed class AddVehicleInput(string makeAndModel, LicensePlate licensePlate, DateTime manufacturingDate) : IUseCaseInput
    {
        /// <summary>
        /// Gets the vehicle make and model (e.g. "Hyundai Tucson").
        /// </summary>
        public string MakeAndModel { get; } = makeAndModel;

        /// <summary>
        /// Gets the license plate value.
        /// </summary>
        public LicensePlate LicensePlate { get; } = licensePlate;

        /// <summary>
        /// Gets the manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; } = manufacturingDate;
    }
}
