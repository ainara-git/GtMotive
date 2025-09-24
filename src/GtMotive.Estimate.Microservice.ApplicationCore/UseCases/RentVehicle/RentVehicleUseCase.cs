using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Extensions;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Events;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Use case for renting a vehicle.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public sealed class RentVehicleUseCase(
        IRentalRepository rentalRepository,
        IDomainService domainService,
        IRentVehicleOutputPort outputPort,
        IBusFactory busFactory,
        IUnitOfWork unitOfWork) : IUseCase<RentVehicleInput>
    {
        /// <summary>
        /// Executes the rent vehicle use case.
        /// </summary>
        /// <param name="input">Input data for renting the vehicle.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(RentVehicleInput input)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(input);

                // 1. If the vehicle is available to rent in the requested date range, get it
                var vehicle = await domainService.GetVehicleIfAvailableForRent(input.LicensePlate, input.Period);
                if (vehicle is null)
                {
                    outputPort.NotFoundHandle("Vehicle not found or not available");
                    return;
                }

                // 2. Ensure customer can rent
                // Business rule: "A customer can only have one active rental" is enforced by RentalsCollection domain object.
                var activeRentals = await rentalRepository.GetActiveRentalsForCustomerAsync(input.CustomerIdNumber);
                activeRentals.EnsureCustomerCanRent(input.CustomerIdNumber);

                // 3. Create rental
                var rental = new Rental(vehicle.Id, input.CustomerIdNumber, input.Period);

                // 4. Persist changes
                await rentalRepository.AddAsync(rental);
                await unitOfWork.Save();

                // 5. Publish domain event (consider using outbox pattern to ensure reliability)
                var vehicleRentedEvent = new VehicleRentedEvent(
                    rental.VehicleId,
                    rental.Id,
                    rental.CustomerIdNumber.Value,
                    rental.Period.StartDate,
                    rental.Period.EndDate);
                await busFactory.PublishDomainEventAsync(vehicleRentedEvent);

                // 6. Build output
                var output = new RentVehicleOutput(
                    rental.Id,
                    vehicle.Id,
                    vehicle.MakeAndModel,
                    rental.CustomerIdNumber,
                    rental.Period);
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
