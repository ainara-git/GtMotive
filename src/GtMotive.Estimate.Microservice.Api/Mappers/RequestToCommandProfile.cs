using AutoMapper;
using GtMotive.Estimate.Microservice.Api.Requests;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;

namespace GtMotive.Estimate.Microservice.Api.Mappers
{
    public sealed class RequestToCommandProfile : Profile
    {
        public RequestToCommandProfile()
        {
            CreateMap<AddVehicleRequest, AddVehicleCommand>();
            CreateMap<RentVehicleRequest, RentVehicleCommand>();
            CreateMap<ReturnVehicleRequest, ReturnVehicleCommand>();
            CreateMap<GetAvailableVehiclesRequest, GetAvailableVehiclesQuery>();
        }
    }
}
