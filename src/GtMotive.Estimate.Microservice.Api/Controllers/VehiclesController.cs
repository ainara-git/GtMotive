using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Api.UseCases.AddVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles;
using GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        // GetAllAvailableVehicles
        [HttpPost("available")]
        public async Task<IActionResult> GetAvailableVehicles([FromBody] GetAvailableVehiclesRequest request)
        {
            var query = mapper.Map<GetAvailableVehiclesQuery>(request);
            var result = await mediator.Send(query);

            return ((IWebApiPresenter)result).ActionResult;
        }

        // AddVehicle
        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] AddVehicleRequest request)
        {
            var command = mapper.Map<AddVehicleCommand>(request);
            var result = await mediator.Send(command);

            return ((IWebApiPresenter)result).ActionResult;
        }

        // RentVehicle
        [HttpPost("rent")]
        public async Task<IActionResult> RentVehicle([FromBody] RentVehicleRequest request)
        {
            var command = mapper.Map<RentVehicleCommand>(request);
            var result = await mediator.Send(command);

            return ((IWebApiPresenter)result).ActionResult;
        }

        // ReturnVehicle
        [HttpPut("return")]
        public async Task<IActionResult> ReturnVehicle([FromBody] ReturnVehicleRequest request)
        {
            var command = mapper.Map<ReturnVehicleCommand>(request);
            var result = await mediator.Send(command);

            return ((IWebApiPresenter)result).ActionResult;
        }
    }
}
