using System;

namespace GtMotive.Estimate.Microservice.Api.Requests
{
    public sealed class AddVehicleRequest
    {
        public string MakeAndModel { get; set; } = string.Empty;

        public string LicensePlate { get; set; } = string.Empty;

        public DateTime? ManufacturingDate { get; set; }
    }
}
