using System;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles
{
    public sealed class GetAvailableVehiclesRequest
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
