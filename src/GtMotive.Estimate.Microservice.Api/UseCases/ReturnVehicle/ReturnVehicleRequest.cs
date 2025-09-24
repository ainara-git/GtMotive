namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    public sealed class ReturnVehicleRequest
    {
        public string LicensePlate { get; set; } = string.Empty;

        public string CustomerIdNumber { get; set; } = string.Empty;
    }
}
