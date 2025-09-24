using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    public interface IMongoService
    {
        IMongoClient MongoClient { get; }

        IMongoDatabase Database { get; }
    }
}
