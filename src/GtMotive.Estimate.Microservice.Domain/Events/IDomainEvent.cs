﻿using System;

namespace GtMotive.Estimate.Microservice.Domain.Events
{
    /// <summary>
    /// Base interface for all domain events.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Gets the unique identifier of the event.
        /// </summary>
        Guid EventId { get; }

        /// <summary>
        /// Gets the date and time when the event occurred.
        /// </summary>
        DateTime OccurredOn { get; }
    }
}
