using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Base class for all domain entities, providing an identifier and identity-based equality.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier of the entity.
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current entity.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if the object is an entity with the same identifier; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is not EntityBase other)
            {
                return false;
            }
            else if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Id != Guid.Empty && Id == other.Id;
        }

        /// <summary>
        /// Returns a hash code for the entity based on its identifier.
        /// </summary>
        /// <returns>An integer hash code.</returns>
        public override int GetHashCode() => Id.GetHashCode();
    }
}
