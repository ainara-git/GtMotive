using System;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Mappers;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Repositories;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Specs
{
    public class VehicleRepositoryInfrastructureTests : InfrastructureTestBase
    {
        private readonly VehicleRepository _vehicleRepository;
        private readonly IMongoDatabase _database;

        public VehicleRepositoryInfrastructureTests(GenericInfrastructureTestServerFixture fixture)
            : base(fixture)
        {
            var mongoService = new MongoService(Options.Create(new MongoDbSettings
            {
                ConnectionString = GenericInfrastructureTestServerFixture.MongoConnectionString,
                DbName = "gtMotiveEstimate"
            }));
            _database = mongoService.Database;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<VehicleProfile>();
            });
            var mapper = mapperConfig.CreateMapper();

            _vehicleRepository = new VehicleRepository(mongoService, mapper);

            // Clean up the collection before each test
            _database.GetCollection<VehicleDocument>("Vehicles").DeleteMany(Builders<VehicleDocument>.Filter.Empty);
        }

        [Fact]
        public async Task AddAndGetVehicleWithValidDataShouldPersistAndRetrieveSuccessfully()
        {
            // Arrange
            var makeAndModel = "Ford Focus";
            var licensePlate = new LicensePlate("5678XYZ");
            var manufacturingDate = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var vehicle = new Vehicle(makeAndModel, licensePlate, manufacturingDate);

            // Act
            await _vehicleRepository.AddAsync(vehicle);
            var retrievedVehicle = await _vehicleRepository.GetByLicensePlateAsync(licensePlate);

            // Assert
            Assert.NotNull(retrievedVehicle);
            Assert.Equal(vehicle.Id, retrievedVehicle.Id);
            Assert.Equal(makeAndModel, retrievedVehicle.MakeAndModel);
            Assert.Equal(licensePlate.Value, retrievedVehicle.LicensePlate.Value);
            Assert.Equal(manufacturingDate, retrievedVehicle.ManufacturingDate);
        }
    }
}
