using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.MongoDb.Documents;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.MongoDb.Repositories
{
    public sealed class VehicleRepository(IMongoService mongoService, IMapper mapper) : IVehicleRepository
    {
        private const string CollectionName = "Vehicles";
        private readonly IMongoCollection<VehicleDocument> _collection = mongoService.Database.GetCollection<VehicleDocument>(CollectionName);

        public async Task AddAsync(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            var vehicleDoc = mapper.Map<VehicleDocument>(vehicle);
            await _collection.InsertOneAsync(vehicleDoc);
        }

        public async Task<VehiclesCollection> GetAllAsync()
        {
            var vehicleDocsList = await _collection.Find(Builders<VehicleDocument>.Filter.Empty).ToListAsync();

            var vehicles = mapper.Map<IEnumerable<Vehicle>>(vehicleDocsList);

            return new VehiclesCollection(vehicles);
        }

        public async Task<Vehicle> GetByLicensePlateAsync(LicensePlate licensePlate)
        {
            ArgumentNullException.ThrowIfNull(licensePlate);

            var filter = Builders<VehicleDocument>.Filter.Eq(v => v.LicensePlate, licensePlate.Value);
            var vehicleDoc = await _collection.Find(filter).FirstOrDefaultAsync();

            return mapper.Map<Vehicle>(vehicleDoc);
        }
    }
}
