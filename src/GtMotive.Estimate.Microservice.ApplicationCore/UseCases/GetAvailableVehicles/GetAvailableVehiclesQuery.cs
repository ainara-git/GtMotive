using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Query to retrieve all vehicles that are currently available for rental within a specified date range.
    /// </summary>
    public sealed class GetAvailableVehiclesQuery(DateTime startDate, DateTime endDate) : IRequest<ICommandResult>
    {
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
