using System;
using GtMotive.Estimate.Microservice.Domain.Common;

namespace GtMotive.Estimate.Microservice.Domain.Events
{
    /// <summary>
    /// Domain event raised when a vehicle is returned.
    /// </summary>
    public sealed class VehicleReturnedEvent(Guid vehicleId, Guid rentalId, string customerIdNumber, DateTime actualReturnDate) : IDomainEvent
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
        /// Gets the customer's identification number.
        /// </summary>
        public string CustomerIdNumber { get; } = customerIdNumber;

        /// <summary>
        /// Gets the actual return date.
        /// </summary>
        public DateTime ActualReturnDate { get; } = actualReturnDate;

        /// <summary>
        /// Gets the date and time when the event occurred.
        /// </summary>
        public DateTime OccurredOn { get; } = DateTimeProvider.Today;
    }
}
