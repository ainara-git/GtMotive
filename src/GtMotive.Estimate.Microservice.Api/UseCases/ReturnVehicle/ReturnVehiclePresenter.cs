using System;
using System.Globalization;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    public sealed class ReturnVehiclePresenter : IReturnVehicleOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(ReturnVehicleOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            var response = new
            {
                output.VehicleId,
                output.VehicleLicensePlate,
                output.CustomerIdNumber,
                ReturnDate = output.ActualReturnDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Message = "Vehicle returned successfully"
            };

            ActionResult = new OkObjectResult(response);
        }

        public void ErrorHandle(string message)
        {
            ActionResult = new ObjectResult(new { Error = message }) { StatusCode = 500 };
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(new { Error = message });
        }
    }
}
