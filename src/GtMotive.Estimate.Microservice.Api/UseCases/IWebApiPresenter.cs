using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases
{
    public interface IWebApiPresenter : ICommandResult
    {
        IActionResult ActionResult { get; }
    }
}
