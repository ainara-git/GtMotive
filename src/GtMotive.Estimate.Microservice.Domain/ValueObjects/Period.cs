using System;
using GtMotive.Estimate.Microservice.Domain.Exceptions;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Represents a period of time as a Value Object (Used e.g. to represent a rental duration).
    /// </summary>
    public sealed record Period
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Period"/> class. Ensures end date is not before start date.
        /// </summary>
        /// <param name="startDate">Start date of the rental.</param>
        /// <param name="endDate">Planned end date of the rental.</param>
        /// <exception cref="DomainException">Thrown if the period is invalid.</exception>
        public Period(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate.Date;
            EndDate = endDate.Date;

            if (EndDate < StartDate)
            {
                throw new DomainException("End date cannot be before start date.");
            }
        }

        /// <summary>
        /// Gets the rental start date.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets the planned rental end date.
        /// </summary>
        public DateTime EndDate { get; }
    }
}
