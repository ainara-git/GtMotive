using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Events;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Extensions
{
    /// <summary>
    /// Extension methods for IBusFactory to simplify domain event publishing.
    /// </summary>
    public static class BusExtensions
    {
        /// <summary>
        /// Publishes a domain event using the appropriate bus client.
        /// </summary>
        /// <param name="busFactory">The bus factory.</param>
        /// <param name="domainEvent">The domain event to publish.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task PublishDomainEventAsync(this IBusFactory busFactory, IDomainEvent domainEvent)
        {
            ArgumentNullException.ThrowIfNull(busFactory);
            ArgumentNullException.ThrowIfNull(domainEvent);

            var bus = busFactory.GetClient(domainEvent.GetType());
            await bus.Send(domainEvent);
        }
    }
}
