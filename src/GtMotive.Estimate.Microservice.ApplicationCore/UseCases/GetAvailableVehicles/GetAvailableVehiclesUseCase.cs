using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Use case to get available vehicles.
    /// </summary>
    public sealed class GetAvailableVehiclesUseCase(
        IDomainService domainService,
        IGetAvailableVehiclesOutputPort outputPort) : IUseCase<GetAvailableVehiclesInput>
    {
        /// <summary>
        /// Executes the GetAllAvailableVehicles use case.
        /// </summary>
        /// <param name="input">Input parameters for the use case.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(GetAvailableVehiclesInput input)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(input);

                // 1. Get the vehicles
                // Business invariant: "Vehicles older than 5 years cannot be in the fleet" is enforced at:
                // - Creation: Vehicle aggregate root constructor
                // - Filtering: VehiclesCollection.FilterOutOldVehicles()
                var availableVehicles = await domainService.GetAllAvailableVehicles(input.AvailabilityPeriod);

                // 4. Create output with the vehicle collection
                var output = new GetAvailableVehiclesOutput(availableVehicles);

                // 5. Build output
                outputPort.StandardHandle(output);
            }
            catch (Exception ex)
            {
                outputPort.ErrorHandle($"ERROR: {ex.Message}");
                throw;
            }
        }
    }
}
