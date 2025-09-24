using System;
using AutoMapper;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Enums;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Documents;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Mappers
{
    public sealed class RentalProfile : Profile
    {
        public RentalProfile()
        {
            // Rental → RentalDocument
            CreateMap<Rental, RentalDocument>()
                .ForMember(dest => dest.CustomerIdNumber, opt => opt.MapFrom(src => src.CustomerIdNumber.Value))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Period.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.Period.EndDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            // RentalDocument → Rental
            CreateMap<RentalDocument, Rental>()
                .ForCtorParam("id", opt => opt.MapFrom(src => src.Id))
                .ForCtorParam("vehicleId", opt => opt.MapFrom(src => src.VehicleId))
                .ForCtorParam("customerIdNumber", opt => opt.MapFrom(src => new PersonalIdentificationNumber(src.CustomerIdNumber)))
                .ForCtorParam("period", opt => opt.MapFrom(src => new Period(src.StartDate, src.EndDate)))
                .ForCtorParam("status", opt => opt.MapFrom(src => Enum.Parse<RentalStatus>(src.Status)))
                .ForCtorParam("actualReturnDate", opt => opt.MapFrom(src => src.ActualReturnDate));
        }
    }
}
