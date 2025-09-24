using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Output data for the GetAllAvailableVehicles use case.
    /// </summary>
    public sealed record GetAvailableVehiclesOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableVehiclesOutput"/> class.
        /// </summary>
        /// <param name="vehicleCollection">The collection of available vehicles.</param>
        public GetAvailableVehiclesOutput(VehiclesCollection vehicleCollection)
        {
            VehicleCollection = vehicleCollection;
        }

        /// <summary>
        /// Gets the collection of available vehicles.
        /// </summary>
        /// <value>The vehicle collection containing all available vehicles.</value>
        public VehiclesCollection VehicleCollection { get; }
    }
}
