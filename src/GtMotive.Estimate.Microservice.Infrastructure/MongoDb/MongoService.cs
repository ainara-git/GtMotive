using System;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    public sealed class MongoService : IMongoService
    {
        public MongoService(IOptions<MongoDbSettings> options)
        {
            ArgumentNullException.ThrowIfNull(options);
            ArgumentNullException.ThrowIfNull(options.Value);
            ArgumentException.ThrowIfNullOrWhiteSpace(options.Value.ConnectionString);
            ArgumentException.ThrowIfNullOrWhiteSpace(options.Value.DbName);

            MongoClient = new MongoClient(options.Value.ConnectionString);
            Database = MongoClient.GetDatabase(options.Value.DbName);
        }

        public IMongoClient MongoClient { get; }

        public IMongoDatabase Database { get; }
    }
}
