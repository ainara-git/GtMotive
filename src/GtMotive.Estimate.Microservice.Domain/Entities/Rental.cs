using System;
using GtMotive.Estimate.Microservice.Domain.Common;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Rental entity acting as Aggregate Root for representing a vehicle rental agreement.
    /// </summary>
    public class Rental : EntityBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier being rented.</param>
        /// <param name="customerIdNumber">The customer's identification number.</param>
        /// <param name="period">The rental period (start and end dates).</param>
        /// <exception cref="ArgumentNullException">Thrown if any parameter is null or empty.</exception>
        public Rental(Guid vehicleId, PersonalIdentificationNumber customerIdNumber, Period period)
        {
            Id = Guid.NewGuid();
            VehicleId = vehicleId != Guid.Empty ? vehicleId : throw new ArgumentNullException(nameof(vehicleId), "vehicleId is required.");
            CustomerIdNumber = customerIdNumber ?? throw new ArgumentNullException(nameof(customerIdNumber), "customerIdNumber is required.");
            Period = period ?? throw new ArgumentNullException(nameof(period), "period is required.");
            Status = RentalStatus.Active;
            ActualReturnDate = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class for rehydration from persistent storage.
        /// </summary>
        /// <param name="id">The rental ID.</param>
        /// <param name="vehicleId">The vehicle identifier being rented.</param>
        /// <param name="customerIdNumber">The customer's identification number.</param>
        /// <param name="period">The rental period (start and end dates).</param>
        /// <param name="status">The rental status.</param>
        /// <param name="actualReturnDate">The actual return date.</param>
        internal Rental(Guid id, Guid vehicleId, PersonalIdentificationNumber customerIdNumber, Period period, RentalStatus status, DateTime actualReturnDate)
        {
            Id = id;
            VehicleId = vehicleId != Guid.Empty ? vehicleId : throw new ArgumentNullException(nameof(vehicleId), "vehicleId is required.");
            CustomerIdNumber = customerIdNumber ?? throw new ArgumentNullException(nameof(customerIdNumber), "customerIdNumber is required.");
            Period = period ?? throw new ArgumentNullException(nameof(period), "period is required.");
            Status = status;
            ActualReturnDate = actualReturnDate;
        }

        /// <summary>
        /// Gets the vehicle identifier being rented.
        /// </summary>
        public Guid VehicleId { get; private set; }

        /// <summary>
        /// Gets the customer's identification number.
        /// </summary>
        public PersonalIdentificationNumber CustomerIdNumber { get; private set; }

        /// <summary>
        /// Gets the rental period (start and end dates).
        /// </summary>
        public Period Period { get; private set; }

        /// <summary>
        /// Gets the actual return date when the vehicle was returned.
        /// </summary>
        public DateTime? ActualReturnDate { get; private set; }

        /// <summary>
        /// Gets the rental status.
        /// </summary>
        public RentalStatus Status { get; private set; }

        /// <summary>
        /// Checks if the rental is currently active.
        /// </summary>
        /// <returns>True if the rental is active.</returns>
        public bool IsActive() => Status == RentalStatus.Active;

        /// <summary>
        /// Checks if the rental is on time (not overdue).
        /// </summary>
        /// <returns>
        /// True if the rental is active and today's date is on or before the expected return date, otherwise false.
        /// </returns>
        public bool IsWithinRentalPeriod() => Status == RentalStatus.Active && DateTimeProvider.Today <= Period.EndDate.Date && DateTimeProvider.Today >= Period.StartDate.Date;

        /// <summary>
        /// Checks if the specified person can return this rental.
        /// </summary>
        /// <param name="returningPersonId">Personal identification number of the person returning the vehicle.</param>
        /// <returns>True if the person can return the rental; otherwise, false.</returns>
        public bool CanBeReturnedBy(PersonalIdentificationNumber returningPersonId) => CustomerIdNumber.Equals(returningPersonId);

        /// <summary>
        /// Marks the rental as completed and sets the return date to today (control state transition).
        /// </summary>
        public void MarkAsCompleted()
        {
            if (!IsActive())
            {
                throw new DomainException("Cannot complete a rental that is not active.");
            }

            Status = RentalStatus.Completed;
            ActualReturnDate = DateTimeProvider.Today;
        }
    }
}
