using System;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.RabbitMq.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace GtMotive.Estimate.Microservice.Infrastructure.RabbitMq
{
    public sealed class RabbitMqBusFactory : IBusFactory, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IAppLogger<RabbitMqBusFactory> _logger;

        public RabbitMqBusFactory(IOptions<RabbitMqSettings> options, IAppLogger<RabbitMqBusFactory> logger)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(options.Value);
            ArgumentException.ThrowIfNullOrWhiteSpace(options.Value.HostName);

            _logger = logger;

            var factory = new ConnectionFactory
            {
                HostName = options.Value.HostName,
                Port = options.Value.Port,
                UserName = options.Value.UserName,
                Password = options.Value.Password
            };

            try
            {
                _connection = factory.CreateConnection();
                _logger.LogInformation("Connected to RabbitMQ");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to RabbitMQ");
                throw;
            }
        }

        public IBus GetClient(Type eventType)
        {
            ArgumentNullException.ThrowIfNull(eventType);

            return new RabbitMqBus(_connection, eventType.Name, _logger);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
