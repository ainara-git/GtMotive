using System;
using GtMotive.Estimate.Microservice.Domain.Common;

namespace GtMotive.Estimate.Microservice.Domain.Events
{
    /// <summary>
    /// Domain event raised when a new vehicle is added to the fleet.
    /// </summary>
    public sealed class VehicleAddedEvent(Guid vehicleId, string makeAndModel, string licensePlate) : IDomainEvent
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
        /// Gets the vehicle make and model.
        /// </summary>
        public string MakeAndModel { get; } = makeAndModel;

        /// <summary>
        /// Gets the vehicle license plate.
        /// </summary>
        public string LicensePlate { get; } = licensePlate;

        /// <summary>
        /// Gets the date and time when the event occurred.
        /// </summary>
        public DateTime OccurredOn { get; } = DateTimeProvider.Today;
    }
}
