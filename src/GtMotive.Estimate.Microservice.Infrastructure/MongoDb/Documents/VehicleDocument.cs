using System;
using MongoDB.Bson.Serialization.Attributes;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents
{
    public sealed class VehicleDocument
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("makeAndModel")]
        public string MakeAndModel { get; set; } = null!;

        [BsonElement("licensePlate")]
        public string LicensePlate { get; set; } = null!;

        [BsonElement("manufacturingDate")]
        public DateTime ManufacturingDate { get; set; }
    }
}
