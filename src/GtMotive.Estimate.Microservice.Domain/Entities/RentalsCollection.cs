using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GtMotive.Estimate.Microservice.Domain.Constants;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Represents a collection of rentals as a first-class domain concept.
    /// Guarantees uniqueness and provides domain-oriented operations.
    /// </summary>
    public sealed class RentalsCollection : IReadOnlyCollection<Rental>
    {
        private readonly List<Rental> _rentals;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalsCollection"/> class. Ensures no duplicate rental identifiers exist.
        /// </summary>
        /// <param name="items">Rentals to include in the collection.</param>
        /// <exception cref="ArgumentException">Thrown if duplicate items are found.</exception>
        public RentalsCollection(IEnumerable<Rental> items)
        {
            ArgumentNullException.ThrowIfNull(items);

            var itemsToAdd = items.ToList();

            if (itemsToAdd.GroupBy(v => v.Id).Any(g => g.Count() > 1))
            {
                throw new ArgumentException("No duplicate items are allowed in the collection.");
            }

            _rentals = [.. itemsToAdd];
        }

        /// <summary>
        /// Gets the number of rentals in the collection.
        /// </summary>
        public int Count => _rentals.Count;

        /// <summary>
        /// Returns an enumerator that iterates through the rentals collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        public IEnumerator<Rental> GetEnumerator() => _rentals.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the rentals collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // Domain-oriented operations

        /// <summary>
        /// Checks if there is any active rental for a specific vehicle within this collection.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle.</param>
        /// <returns>True if an active rental for the vehicle exists; otherwise, false.</returns>
        public bool HasActiveRentalForVehicle(Guid vehicleId)
        {
            return _rentals.Any(r => r.VehicleId == vehicleId && r.IsActive());
        }

        /// <summary>
        /// Checks if the customer has reached the maximum number of active rentals allowed.
        /// </summary>
        /// <param name="customerIdNumber">The personal identification number of the customer.</param>
        /// <returns>True if the customer can rent more; false if the limit is reached.</returns>
        public bool CanCustomerRentMore(PersonalIdentificationNumber customerIdNumber)
        {
            var customerActiveRentals = _rentals.Count(r => r.IsActive() && r.CustomerIdNumber == customerIdNumber);

            return customerActiveRentals < DomainConstants.MaxActiveRentals;
        }

        /// <summary>
        /// Ensures the customer can rent a vehicle without violating the "one active rental" business rule.
        /// Throws <see cref="DomainException"/> if the customer already has the maximum active rentals.
        /// </summary>
        /// <param name="customerIdNumber">The personal identification number of the customer.</param>
        /// <exception cref="DomainException">Thrown if the customer has reached the maximum active rentals.</exception>
        public void EnsureCustomerCanRent(PersonalIdentificationNumber customerIdNumber)
        {
            if (!CanCustomerRentMore(customerIdNumber))
            {
                throw new DomainException($"A customer can only have {DomainConstants.MaxActiveRentals} active rental(s)");
            }
        }
    }
}
