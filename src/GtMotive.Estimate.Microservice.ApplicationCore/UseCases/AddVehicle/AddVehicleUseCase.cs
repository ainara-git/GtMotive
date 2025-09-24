using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Extensions;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Events;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle
{
    /// <summary>
    /// Use case for adding a new vehicle to the fleet.
    /// </summary>
    public sealed class AddVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IAddVehicleOutputPort outputPort,
        IUnitOfWork unitOfWork,
        IBusFactory busFactory) : IUseCase<AddVehicleInput>
    {
        /// <summary>
        /// Executes the AddVehicle use case.
        /// </summary>
        /// <param name="input">Input data for creating the vehicle.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(AddVehicleInput input)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(input);

                // 1. Check if it already exits
                var searchedVehicle = await vehicleRepository.GetByLicensePlateAsync(input.LicensePlate);
                if (searchedVehicle is not null)
                {
                    outputPort.ErrorHandle($"ERROR: Vehicle already exists.");
                    return;
                }

                // 2. Create new vehicle
                // Business invariant: "Vehicles older than 5 years cannot be in the fleet" is enforced at:
                // - Creation: Vehicle aggregate root constructor
                // - Filtering: VehiclesCollection.FilterOutOldVehicles()
                var vehicle = new Vehicle(
                     input.MakeAndModel,
                     input.LicensePlate,
                     input.ManufacturingDate);

                // 4. Persist entity
                await vehicleRepository.AddAsync(vehicle);
                await unitOfWork.Save();

                // 5. Publish domain event (Consider using outbox pattern to ensure reliability)
                var vehicleAddedEvent = new VehicleAddedEvent(
                    vehicle.Id,
                    vehicle.MakeAndModel,
                    vehicle.LicensePlate.Value);

                await busFactory.PublishDomainEventAsync(vehicleAddedEvent);

                // 6. Build output
                var output = new AddVehicleOutput(
                    vehicle.Id,
                    vehicle.MakeAndModel,
                    vehicle.LicensePlate,
                    vehicle.ManufacturingDate);

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
