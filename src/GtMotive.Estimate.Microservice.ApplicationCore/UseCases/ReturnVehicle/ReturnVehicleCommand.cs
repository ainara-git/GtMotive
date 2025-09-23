using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Represents a command to return a rented vehicle.
    /// </summary>
    public sealed class ReturnVehicleCommand(string licensePlate, string customerIdNumber) : IRequest<ICommandResult>
    {
        /// <summary>
        /// Gets the license plate of the vehicle to return.
        /// </summary>
        public string LicensePlate { get; } = licensePlate;

        /// <summary>
        /// Gets the customer's identification number.
        /// </summary>
        public string CustomerIdNumber { get; } = customerIdNumber;
    }
}
