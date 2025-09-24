using System;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Represents a command to rent a vehicle by license plate.
    /// </summary>
    public sealed class RentVehicleCommand(string licensePlate, string customerIdNumber, DateTime startDate, DateTime endDate) : IRequest<ICommandResult>
    {
        /// <summary>
        /// Gets the license plate of the vehicle to rent.
        /// </summary>
        public string LicensePlate { get; } = licensePlate;

        /// <summary>
        /// Gets the customer's identification number.
        /// </summary>
        public string CustomerIdNumber { get; } = customerIdNumber;

        /// <summary>
        /// Gets the rental start date.
        /// </summary>
        public DateTime StartDate { get; } = startDate;

        /// <summary>
        /// Gets the rental end date.
        /// </summary>
        public DateTime EndDate { get; } = endDate;
    }
}
