using System;
using GtMotive.Estimate.Microservice.Domain.Common;

namespace GtMotive.Estimate.Microservice.Domain.Events
{
    /// <summary>
    /// Domain event raised when a vehicle is rented.
    /// </summary>
    public sealed class VehicleRentedEvent(Guid vehicleId, Guid rentalId, string customerIdNumber, DateTime startDate, DateTime endDate) : IDomainEvent
    {
        /// <summary>
        /// Gets the unique identifier of the event.
        /// </summary>
        public Guid EventId { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        public Guid RentalId { get; } = rentalId;

        /// <summary>
        /// Gets the renter identification number.
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

        /// <summary>
        /// Gets the date and time when the event occurred.
        /// </summary>
        public DateTime OccurredOn { get; } = DateTimeProvider.Today;
    }
}
