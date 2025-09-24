using System;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.RabbitMq.NoOpBus
{
    /// <summary>
    /// Used in development environment (and test project).
    /// </summary>
    internal sealed class NoOpBusFactory : IBusFactory
    {
        private readonly IAppLogger<NoOpBusFactory> _logger;

        public NoOpBusFactory(IAppLogger<NoOpBusFactory> logger)
        {
            _logger = logger;
            _logger.LogInformation("NoOpBusFactory initialized. RabbitMQ messaging is bypassed for this environment.");
        }

        public IBus GetClient(Type eventType)
        {
            ArgumentNullException.ThrowIfNull(eventType);
            return new NoOpBus(eventType.Name, _logger);
        }
    }
}
