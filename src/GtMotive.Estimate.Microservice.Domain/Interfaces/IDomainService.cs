using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Interface for domain services that handle cross-aggregate business operations.
    /// </summary>
    public interface IDomainService
    {
        /// <summary>
        /// Retrieves available vehicles for rent during the specified period.
        /// </summary>
        /// <param name="availabilityPeriod">The period to check vehicle availability.</param>
        /// <returns>A collection of vehicles available for rent during the period.</returns>
        Task<VehiclesCollection> GetAllAvailableVehicles(Period availabilityPeriod);

        /// <summary>
        /// Retrieves the current active rental for a vehicle identified by its license plate.
        /// </summary>
        /// <param name="licensePlate">The license plate of the vehicle to search for.</param>
        /// <returns>The current active rental for the specified vehicle.</returns>
        Task<Rental> GetCurrentRentalByLicensePlateAsync(LicensePlate licensePlate);

        /// <summary>
        /// Retrieves a vehicle if it is available for rent during the specified period.
        /// </summary>
        /// <param name="licensePlate">The license plate of the vehicle to check availability for.</param>
        /// <param name="period">The period during which the vehicle should be available.</param>
        /// <returns>
        /// The <see cref="Vehicle"/> if it is available for rent during the specified period.
        /// </returns>
        Task<Vehicle> GetVehicleIfAvailableForRent(LicensePlate licensePlate, Period period);
    }
}
