using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Mongo2Go;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure
{
    public sealed class GenericInfrastructureTestServerFixture : IDisposable
    {
        private readonly MongoDbRunner _mongoRunner;

        public GenericInfrastructureTestServerFixture()
        {
            _mongoRunner = MongoDbRunner.Start();
            MongoConnectionString = _mongoRunner.ConnectionString;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseEnvironment("IntegrationTest")
                .UseDefaultServiceProvider(options => { options.ValidateScopes = true; })
                .ConfigureAppConfiguration((context, builder) => { builder.AddEnvironmentVariables(); })
                .UseStartup<Startup>();

            Server = new TestServer(hostBuilder);
        }

        public static string MongoConnectionString { get; private set; }

        public TestServer Server { get; }

        public void Dispose()
        {
            Server?.Dispose();
            _mongoRunner?.Dispose();
        }
    }
}
