using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.AddVehicle
{
    public sealed class AddVehiclePresenter : IAddVehicleOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(AddVehicleOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            var response = new
            {
                output.VehicleId,
                output.MakeAndModel,
                LicensePlate = output.LicensePlate.ToString(),
                output.ManufacturingDate
            };

            ActionResult = new CreatedResult($"/api/vehicles/{output.VehicleId}", response);
        }

        public void ErrorHandle(string message)
        {
            ActionResult = new ObjectResult(new { Error = message }) { StatusCode = 500 };
        }
    }
}
