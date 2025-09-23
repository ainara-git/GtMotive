using AutoMapper;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using GtMotive.Estimate.Microservice.Infrastructure.Persistence.MongoDb.Documents;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence.MongoDb.Mappers
{
    public sealed class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            // Vehicle → VehicleDocument
            CreateMap<Vehicle, VehicleDocument>()
                .ForMember(dest => dest.ManufacturingDate, opt => opt.MapFrom(src => src.ManufacturingDate))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.LicensePlate.Value));

            // VehicleDocument → Vehicle (usando factory para rehidratar)
            CreateMap<VehicleDocument, Vehicle>()
                  .ForCtorParam("id", opt => opt.MapFrom(src => src.Id))
                  .ForCtorParam("makeAndModel", opt => opt.MapFrom(src => src.MakeAndModel))
                  .ForCtorParam("licensePlate", opt => opt.MapFrom(src => new LicensePlate(src.LicensePlate)))
                  .ForCtorParam("manufacturingDate", opt => opt.MapFrom(src => src.ManufacturingDate));
        }
    }
}
