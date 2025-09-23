using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Input data to get the list of available vehicles in the fleet.
    /// </summary>
    public sealed class GetAvailableVehiclesInput(Period availabilityPeriod) : IUseCaseInput
    {
        /// <summary>
        /// Gets the rental period.
        /// </summary>
        public Period AvailabilityPeriod { get; } = availabilityPeriod;
    }
}
