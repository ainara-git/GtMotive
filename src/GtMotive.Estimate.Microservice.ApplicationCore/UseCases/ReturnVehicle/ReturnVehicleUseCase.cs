using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Extensions;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using GtMotive.Estimate.Microservice.Domain.Events;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Executes the return vehicle use case.
    /// </summary>
    public sealed class ReturnVehicleUseCase(
        IRentalRepository rentalRepository,
        IDomainService domainService,
        IReturnVehicleOutputPort outputPort,
        IBusFactory busFactory,
        IUnitOfWork unitOfWork) : IUseCase<ReturnVehicleInput>
    {
        /// <summary>
        /// Executes the return vehicle use case.
        /// </summary>
        /// <param name="input">Input data for returning the vehicle.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(ReturnVehicleInput input)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(input);

                // 1. Get the current active rental for the vehicle
                var rental = await domainService.GetCurrentRentalByLicensePlateAsync(input.LicensePlate);
                if (rental is null)
                {
                    outputPort.NotFoundHandle($"No active rental found for vehicle {input.LicensePlate}");
                    return;
                }

                // 2. Check If the person returning is the one who rented the vehicle originally
                if (!rental.CanBeReturnedBy(input.CustomerIdNumber))
                {
                    outputPort.ErrorHandle($"Customer {input.CustomerIdNumber} is not allowed to return the vehicle rented by {rental.CustomerIdNumber}");
                    return;
                }

                // 3. Ensure rental is on time, if not early exit.
                if (!rental.IsWithinRentalPeriod())
                {
                    outputPort.ErrorHandle("Is not allowed to complete a rental operation out of the rental period.");
                    return;
                }

                // 4. Complete return
                rental.MarkAsCompleted();

                // 5. Save changes
                await rentalRepository.UpdateAsync(rental);
                await unitOfWork.Save();

                // 6. Publish domain event (consider using an outbox pattern to ensure reliability)
                var vehicleReturnedEvent = new VehicleReturnedEvent(
                    rental.VehicleId,
                    rental.Id,
                    rental.CustomerIdNumber.Value,
                    actualReturnDate: DateTime.Today);
                await busFactory.PublishDomainEventAsync(vehicleReturnedEvent);

                // 8. Build output
                var output = new ReturnVehicleOutput(
                    rental.VehicleId,
                    input.LicensePlate.ToString(),
                    rental.CustomerIdNumber.ToString(),
                    actualReturnDate: DateTime.Today);

                outputPort.StandardHandle(output);
            }
            catch (Exception ex)
            {
                outputPort.ErrorHandle($"ERROR: {ex.Message}.");
                throw;
            }
        }
    }
}
