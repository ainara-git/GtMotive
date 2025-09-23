using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for rental operations.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Adds a new rental to the repository.
        /// </summary>
        /// <param name="rental">The rental to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Rental rental);

        /// <summary>
        /// Updates an existing rental in the repository.
        /// </summary>
        /// <param name="rental">The rental to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(Rental rental);

        /// <summary>
        /// Gets all active rental within a given date range.
        /// </summary>
        /// <param name="startDate">Start date to look for.</param>
        /// <param name="endDate">End date to look for.</param>
        /// <returns>A domain collection containing all the active rentals within the given data range.</returns>
        Task<RentalsCollection> GetActiveRentalsInRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets the active rental of the given vehicle.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The active rental if found.</returns>
        Task<Rental> GetActiveRentalForVehicleAsync(Guid vehicleId);

        /// <summary>
        /// Gets all active rentals of a customer.
        /// </summary>
        /// <param name="customerIdNumber">Customer's Personal Identification Number.</param>
        /// <returns>A domain collections containing all the active rentals for the given customer.</returns>
        Task<RentalsCollection> GetActiveRentalsForCustomerAsync(PersonalIdentificationNumber customerIdNumber);
    }
}
