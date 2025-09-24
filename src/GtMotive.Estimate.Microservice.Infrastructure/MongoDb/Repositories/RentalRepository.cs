using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Repositories
{
    public sealed class RentalRepository(IMongoService mongoService, IMapper mapper) : IRentalRepository
    {
        private const string CollectionName = "Rentals";
        private readonly IMongoCollection<RentalDocument> _collection = mongoService.Database.GetCollection<RentalDocument>(CollectionName);

        public async Task AddAsync(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);

            var rentalDoc = mapper.Map<RentalDocument>(rental);
            await _collection.InsertOneAsync(rentalDoc);
        }

        public async Task UpdateAsync(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);

            var rentalDoc = mapper.Map<RentalDocument>(rental);
            var filter = Builders<RentalDocument>.Filter.Eq(r => r.Id, rental.Id);
            var result = await _collection.ReplaceOneAsync(filter, rentalDoc);

            if (result.ModifiedCount == 0)
            {
                throw new InvalidOperationException($"Rental with ID {rental.Id} not found.");
            }
        }

        public async Task<RentalsCollection> GetActiveRentalsInRangeAsync(DateTime startDate, DateTime endDate)
        {
            var filterBuilder = Builders<RentalDocument>.Filter;

            var filter = filterBuilder.Lte(r => r.StartDate, endDate) &
                         filterBuilder.Gte(r => r.EndDate, startDate) &
                         filterBuilder.Eq(r => r.Status, RentalStatus.Active.ToString());

            var rentalDocsList = await _collection.Find(filter).ToListAsync();

            var rentals = mapper.Map<IEnumerable<Rental>>(rentalDocsList);

            return new RentalsCollection(rentals);
        }

        public async Task<Rental> GetActiveRentalForVehicleAsync(Guid vehicleId)
        {
            var filter = Builders<RentalDocument>.Filter.And(
                Builders<RentalDocument>.Filter.Eq(r => r.VehicleId, vehicleId),
                Builders<RentalDocument>.Filter.Eq(r => r.Status, RentalStatus.Active.ToString()));

            var rentalDoc = await _collection.Find(filter).FirstOrDefaultAsync();

            return mapper.Map<Rental>(rentalDoc);
        }

        public async Task<RentalsCollection> GetActiveRentalsForCustomerAsync(PersonalIdentificationNumber customerIdNumber)
        {
            ArgumentNullException.ThrowIfNull(customerIdNumber);

            var filter = Builders<RentalDocument>.Filter.And(
                Builders<RentalDocument>.Filter.Eq(r => r.CustomerIdNumber, customerIdNumber.Value),
                Builders<RentalDocument>.Filter.Eq(r => r.Status, RentalStatus.Active.ToString()));

            var rentalDocsList = await _collection.Find(filter).ToListAsync();
            var rentals = mapper.Map<IEnumerable<Rental>>(rentalDocsList);

            return new RentalsCollection(rentals);
        }
    }
}
