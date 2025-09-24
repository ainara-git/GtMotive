using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure.RabbitMq.NoOpBus
{
    /// <summary>
    /// IBus implementation that performs no operations. Used in development environment (and test project).
    /// </summary>
    internal sealed class NoOpBus(string eventType, IAppLogger<NoOpBusFactory> logger) : IBus
    {
        private readonly IAppLogger<NoOpBusFactory> _logger = logger;
        private readonly string _eventType = eventType;

        public Task Send(object message)
        {
            _logger.LogDebug("NoOpBus: Event {EventType} was 'sent' (no-op) for testing purposes.", _eventType);
            return Task.CompletedTask;
        }
    }
}
