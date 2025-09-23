using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for vehicle operations.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a new vehicle to the repository.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Vehicle vehicle);

        /// <summary>
        /// Gets all vehicles.
        /// </summary>
        /// <returns>All the vehicles.</returns>
        Task<VehiclesCollection> GetAllAsync();

        /// <summary>
        /// Gets a vehicle by its license plate.
        /// </summary>
        /// <param name="licensePlate">The license plate to search for.</param>
        /// <returns>The vehicle if found.</returns>
        Task<Vehicle> GetByLicensePlateAsync(LicensePlate licensePlate);
    }
}
