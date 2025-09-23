using System;
using System.Linq;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles
{
    public sealed class GetAvailableVehiclesPresenter : IGetAvailableVehiclesOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(GetAvailableVehiclesOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            var vehicleResponses = output.VehicleCollection.Select(vehicle => new
            {
                vehicle.Id,
                vehicle.MakeAndModel,
                LicensePlate = vehicle.LicensePlate.ToString(),
                vehicle.ManufacturingDate
            }).ToList();

            ActionResult = new OkObjectResult(new
            {
                Message = $"{output.VehicleCollection.Count} vehicles available for rental",
                Vehicles = vehicleResponses,
                TotalAvailable = output.VehicleCollection.Count
            });
        }

        public void ErrorHandle(string message)
        {
            ActionResult = new ObjectResult(new { Error = message }) { StatusCode = 500 };
        }
    }
}
