using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using RabbitMQ.Client;

namespace GtMotive.Estimate.Microservice.Infrastructure.RabbitMq
{
    public sealed class RabbitMqBus(IConnection connection, string queueName, IAppLogger<RabbitMqBusFactory> logger) : IBus
    {
        public async Task Send(object message)
        {
            ArgumentNullException.ThrowIfNull(message);

            using var channel = connection.CreateModel();

            // Declare the queue, if it doesn't exist it will be created
            channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: queueName,
                basicProperties: null,
                body: body);

            logger.LogInformation("Event published to queue {QueueName}: {EventType}", queueName, message.GetType().Name);

            await Task.CompletedTask;
        }
    }
}
