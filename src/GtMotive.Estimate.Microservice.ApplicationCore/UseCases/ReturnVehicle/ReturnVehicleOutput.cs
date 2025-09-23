using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output data for the return vehicle use case.
    /// </summary>
    public sealed class ReturnVehicleOutput(
        Guid vehicleId,
        string vehicleLicensePlate,
        string customerIdNumber,
        DateTime actualReturnDate) : IUseCaseOutput
    {
        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the vehicle license plate.
        /// </summary>
        public string VehicleLicensePlate { get; } = vehicleLicensePlate;

        /// <summary>
        /// Gets the customer's identification number.
        /// </summary>
        public string CustomerIdNumber { get; } = customerIdNumber;

        /// <summary>
        /// Gets the actual return date.
        /// </summary>
        public DateTime ActualReturnDate { get; } = actualReturnDate;
    }
}
