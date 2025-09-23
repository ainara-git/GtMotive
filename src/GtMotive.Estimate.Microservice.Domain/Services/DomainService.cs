using System;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Services
{
    /// <summary>
    /// Encapsulates business rules and operations that span multiple aggregates within the rental domain context.
    /// </summary>
    /// <param name="vehicleRepository">The vehicle repository for data access.</param>
    /// <param name="rentalRepository">The rental repository for data access.</param>
    public class DomainService(IVehicleRepository vehicleRepository, IRentalRepository rentalRepository) : IDomainService
    {
        /// <summary>
        /// Retrieves available vehicles for rent during the specified period.
        /// </summary>
        /// <param name="availabilityPeriod">The period to check vehicle availability.</param>
        /// <returns>A collection of vehicles available for rent during the period.</returns>
        public async Task<VehiclesCollection> GetAllAvailableVehicles(Period availabilityPeriod)
        {
            ArgumentNullException.ThrowIfNull(availabilityPeriod);

            var allVehicles = await vehicleRepository.GetAllAsync();
            var activeRentals = await rentalRepository.GetActiveRentalsInRangeAsync(availabilityPeriod.StartDate, availabilityPeriod.EndDate);
            var rentedVehicleIds = activeRentals.Select(r => r.VehicleId).Distinct();

            return allVehicles.GetAvailableVehiclesForRental(rentedVehicleIds);
        }

        /// <summary>
        /// Retrieves the current active rental for a vehicle identified by its license plate.
        /// </summary>
        /// <param name="licensePlate">The license plate of the vehicle to search for.</param>
        /// <returns>The current active rental for the specified vehicle if any.</returns>
        public async Task<Rental> GetCurrentRentalByLicensePlateAsync(LicensePlate licensePlate)
        {
            ArgumentNullException.ThrowIfNull(licensePlate);

            var vehicle = await vehicleRepository.GetByLicensePlateAsync(licensePlate);
            if (vehicle != null)
            {
                return await rentalRepository.GetActiveRentalForVehicleAsync(vehicle.Id);
            }

            return null;
        }

        /// <summary>
        /// Retrieves a vehicle if it is available for rent during the specified period.
        /// </summary>
        /// <param name="licensePlate">The license plate of the vehicle to check availability for.</param>
        /// <param name="period">The period during which the vehicle should be available.</param>
        /// <returns>
        /// The <see cref="Vehicle"/> if it is available for rent during the specified period.
        /// </returns>
        public async Task<Vehicle> GetVehicleIfAvailableForRent(LicensePlate licensePlate, Period period)
        {
            ArgumentNullException.ThrowIfNull(licensePlate);
            ArgumentNullException.ThrowIfNull(period);

            var vehicle = await vehicleRepository.GetByLicensePlateAsync(licensePlate);

            if (vehicle == null || vehicle.IsTooOldForFleet)
            {
                return null;
            }

            var rentalsInPeriod = await rentalRepository.GetActiveRentalsInRangeAsync(period.StartDate.Date, period.EndDate.Date);

            return rentalsInPeriod.HasActiveRentalForVehicle(vehicle.Id)
                ? null
                : vehicle;
        }
    }
}
