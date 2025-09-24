using System;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    public sealed class RentVehicleRequest
    {
        public string LicensePlate { get; set; } = string.Empty;

        public string CustomerIdNumber { get; set; } = string.Empty;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
