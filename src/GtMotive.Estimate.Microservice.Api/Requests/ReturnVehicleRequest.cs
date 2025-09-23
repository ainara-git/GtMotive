namespace GtMotive.Estimate.Microservice.Api.Requests
{
    public sealed class ReturnVehicleRequest
    {
        public string LicensePlate { get; set; } = string.Empty;

        public string CustomerIdNumber { get; set; } = string.Empty;
    }
}
