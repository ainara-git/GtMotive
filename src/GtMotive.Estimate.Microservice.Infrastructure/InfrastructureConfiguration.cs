using System;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Interfaces;
using GtMotive.Estimate.Microservice.Infrastructure.Logging;
using GtMotive.Estimate.Microservice.Infrastructure.Messaging;
using GtMotive.Estimate.Microservice.Infrastructure.Messaging.NoOpBus;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.MongoDb.Mappers;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.MongoDb.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.Telemetry;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        [ExcludeFromCodeCoverage]
        public static IInfrastructureBuilder AddBaseInfrastructure(
            this IServiceCollection services,
            bool isDevelopment)
        {
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            if (!isDevelopment)
            {
                services.AddScoped<ITelemetry, AppTelemetry>();
            }
            else
            {
                services.AddScoped<ITelemetry, NoOpTelemetry>();
            }

            services.AddMongo();
            services.AddRabbitMq(isDevelopment);

            return new InfrastructureBuilder(services);
        }

        private static void AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            services.AddSingleton<IMongoService, MongoService>();
            services.AddScoped(provider => provider.GetRequiredService<IMongoService>().Database);
            services.AddScoped<IUnitOfWork, MongoUnitOfWork>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddAutoMapper(typeof(VehicleProfile).Assembly);
            services.AddAutoMapper(typeof(VehicleProfile).Assembly, typeof(RentalProfile).Assembly);
        }

        private static void AddRabbitMq(this IServiceCollection services, bool isDevelopment)
        {
            if (isDevelopment)
            {
                services.AddScoped<IBusFactory, NoOpBusFactory>();
            }
            else
            {
                services.AddScoped<IBusFactory, RabbitMqBusFactory>();
            }
        }

        private sealed class InfrastructureBuilder(IServiceCollection services) : IInfrastructureBuilder
        {
            public IServiceCollection Services { get; } = services;
        }
    }
}
