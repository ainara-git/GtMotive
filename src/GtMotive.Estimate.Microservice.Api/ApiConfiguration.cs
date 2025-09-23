using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using GtMotive.Estimate.Microservice.Api.Authorization;
using GtMotive.Estimate.Microservice.Api.DependencyInjection;
using GtMotive.Estimate.Microservice.Api.Filters;
using GtMotive.Estimate.Microservice.ApplicationCore;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common.Behaviours;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.Api
{
    [ExcludeFromCodeCoverage]
    public static class ApiConfiguration
    {
        public static void ConfigureControllers(MvcOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);

            options.Filters.Add<BusinessExceptionFilter>();
        }

        public static IMvcBuilder WithApiControllers(this IMvcBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.AddApplicationPart(typeof(ApiConfiguration).GetTypeInfo().Assembly);

            AddApiDependencies(builder.Services);

            return builder;
        }

        public static void AddApiDependencies(this IServiceCollection services)
        {
            services.AddAuthorization(AuthorizationOptionsExtensions.Configure);

            // AutoMapper for mappings API → Command
            services.AddAutoMapper(typeof(ApiConfiguration).GetTypeInfo().Assembly);

            // MediatR pointing to ApplicationCore layer
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationConfiguration).GetTypeInfo().Assembly));

            // FluentValidation
            services.AddValidatorsFromAssembly(typeof(ApplicationConfiguration).GetTypeInfo().Assembly);

            // PipelineBehavior for validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // UseCases and Presenters
            services.AddUseCases();
            services.AddPresenters();
        }
    }
}
