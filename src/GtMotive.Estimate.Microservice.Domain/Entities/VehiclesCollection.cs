using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GtMotive.Estimate.Microservice.Domain.Common;
using GtMotive.Estimate.Microservice.Domain.Constants;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Represents a collection of vehicles as a first-class domain concept.
    /// Guarantees uniqueness and provides domain-oriented operations.
    /// </summary>
    public sealed class VehiclesCollection : IReadOnlyCollection<Vehicle>
    {
        private readonly List<Vehicle> _vehicles;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesCollection"/> class. Ensures no duplicate vehicle identifiers exist.
        /// </summary>
        /// <param name="items">Vehicles to include in the collection.</param>
        /// <exception cref="ArgumentException">Thrown if duplicate items are found.</exception>
        public VehiclesCollection(IEnumerable<Vehicle> items)
        {
            ArgumentNullException.ThrowIfNull(items);

            var itemsToAdd = items.ToList();

            if (itemsToAdd.GroupBy(v => v.Id).Any(g => g.Count() > 1))
            {
                throw new ArgumentException("No duplicate items are allowed in the collection.");
            }

            if (itemsToAdd.GroupBy(v => v.LicensePlate).Any(g => g.Count() > 1))
            {
                throw new ArgumentException("No duplicate license plates are allowed in the collection.");
            }

            _vehicles = [.. itemsToAdd];
        }

        /// <summary>
        /// Gets the number of vehicles in the collection.
        /// </summary>
        public int Count => _vehicles.Count;

        /// <summary>
        /// Returns an enumerator that iterates through the vehicles collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        public IEnumerator<Vehicle> GetEnumerator() => _vehicles.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the vehicles collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // Domain-oriented operations

        /// <summary>
        /// Filters out vehicles that are older than 5 years.
        /// </summary>
        /// <returns>A new <see cref="VehiclesCollection"/> containing only the vehicles that are not too old to be rented.</returns>
        public VehiclesCollection FilterOutOldVehicles()
        {
            var cutoffDate = DateTimeProvider.Today.AddYears(-DomainConstants.MaxFleetAgeYears);
            var recentVehicles = _vehicles.Where(v => v.ManufacturingDate >= cutoffDate).ToList();
            return new VehiclesCollection(recentVehicles);
        }

        /// <summary>
        /// Filters out vehicles that are present in the provided list of rented vehicle IDs.
        /// </summary>
        /// <param name="rentedVehicleIds">A collection of IDs of vehicles that are currently rented.</param>
        /// <returns>A new <see cref="VehiclesCollection"/> containing only the vehicles that are not rented.</returns>
        public VehiclesCollection FilterOutRentedVehicles(IEnumerable<Guid> rentedVehicleIds)
        {
            ArgumentNullException.ThrowIfNull(rentedVehicleIds);
            var available = _vehicles.Where(v => !rentedVehicleIds.Contains(v.Id)).ToList();
            return new VehiclesCollection(available);
        }

        /// <summary>
        /// Gets the vehicles available for rental: excludes those that are too old and currently rented.
        /// </summary>
        /// <param name="rentedVehicleIds">IDs of vehicles that are rented (from a collection of active rentals).</param>
        /// <returns>A new <see cref="VehiclesCollection"/> with vehicles eligible for new rental.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="rentedVehicleIds"/> is null.</exception>
        public VehiclesCollection GetAvailableVehiclesForRental(IEnumerable<Guid> rentedVehicleIds)
        {
            ArgumentNullException.ThrowIfNull(rentedVehicleIds);

            return FilterOutOldVehicles().FilterOutRentedVehicles(rentedVehicleIds);
        }
    }
}
