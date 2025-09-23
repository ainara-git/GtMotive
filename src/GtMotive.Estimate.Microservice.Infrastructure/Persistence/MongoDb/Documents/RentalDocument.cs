using System;
using MongoDB.Bson.Serialization.Attributes;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.MongoDb.Documents
{
    public sealed class RentalDocument
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("vehicleId")]
        public Guid VehicleId { get; set; }

        [BsonElement("CustomerIdNumber")]
        public string CustomerIdNumber { get; set; } = null!;

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }

        [BsonElement("actualReturnDate")]
        public DateTime? ActualReturnDate { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = null!;
    }
}
