using System;
using System.Globalization;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    public sealed class RentVehiclePresenter : IRentVehicleOutputPort, IWebApiPresenter
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(RentVehicleOutput output)
        {
            ArgumentNullException.ThrowIfNull(output);

            var response = new
            {
                output.RentalId,
                output.VehicleId,
                output.VehicleMakeAndModel,
                CustomerIdNumber = output.CustomerIdNumber.ToString(),
                RentalPeriod = new
                {
                    StartDate = output.Period.StartDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                    EndDate = output.Period.EndDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)
                }
            };

            ActionResult = new CreatedResult($"/api/rentals/{output.RentalId}", response);
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
