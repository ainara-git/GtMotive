using System;

namespace GtMotive.Estimate.Microservice.Api.Requests
{
    public sealed class RentVehicleRequest
    {
        public string LicensePlate { get; set; } = string.Empty;

        public string CustomerIdNumber { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
