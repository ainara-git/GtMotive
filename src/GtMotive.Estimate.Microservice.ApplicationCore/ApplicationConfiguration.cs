using System;
using System.Diagnostics.CodeAnalysis;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.ApplicationCore
{
    /// <summary>
    /// Adds Use Cases classes.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// Adds Use Cases to the ServiceCollection.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>The modified instance.</returns>
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            // AddVehicleUseCase
            services.AddScoped<AddVehicleUseCase>();
            services.AddScoped<AddVehicleCommandHandler>();

            // GetAvailableVehiclesUseCase
            services.AddScoped<GetAvailableVehiclesUseCase>();
            services.AddScoped<GetAvailableVehiclesQueryHandler>();

            // RentVehicleUseCase
            services.AddScoped<RentVehicleUseCase>();
            services.AddScoped<RentVehicleCommandHandler>();

            // ReturnVehicleUseCase
            services.AddScoped<ReturnVehicleUseCase>();
            services.AddScoped<ReturnVehicleCommandHandler>();

            // Domain Services
            services.AddScoped<IDomainService, DomainService>();

            return services;
        }
    }
}
