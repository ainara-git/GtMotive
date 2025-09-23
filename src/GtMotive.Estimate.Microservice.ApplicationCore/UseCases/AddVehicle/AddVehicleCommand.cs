using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle
{
    /// <summary>
    /// Represents a command to add a new vehicle with specified details.
    /// </summary>
    public sealed class AddVehicleCommand(string makeAndModel, string licensePlate, DateTime manufacturingDate) : IRequest<ICommandResult>
    {
        /// <summary>
        /// Gets the make and model of the vehicle.
        /// </summary>
        public string MakeAndModel { get; } = makeAndModel;

        /// <summary>
        /// Gets the license plate of the vehicle.
        /// </summary>
        public string LicensePlate { get; } = licensePlate;

        /// <summary>
        /// Gets the manufacturing date of the product.
        /// </summary>
        public DateTime ManufacturingDate { get; } = manufacturingDate;
    }
}
