using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Handles the execution of GetAvailableVehiclesQuery.
    /// </summary>
    public sealed class GetAvailableVehiclesQueryHandler(GetAvailableVehiclesUseCase useCase, IGetAvailableVehiclesOutputPort outputPort) : IRequestHandler<GetAvailableVehiclesQuery, ICommandResult>
    {
        /// <summary>
        /// Handles the GetAvailableVehiclesQuery by executing the corresponding use case.
        /// </summary>
        /// <param name="query">The query requesting available vehicles information.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when the query is null.</exception>
        public async Task<ICommandResult> Handle(GetAvailableVehiclesQuery query, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(query);

            var input = new GetAvailableVehiclesInput(new Period(query.StartDate, query.EndDate));
            await useCase.Execute(input);

            return (ICommandResult)outputPort;
        }
    }
}
