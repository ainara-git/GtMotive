using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Handles the execution of RentVehicleCommand.
    /// </summary>
    public sealed class RentVehicleCommandHandler(RentVehicleUseCase useCase, IRentVehicleOutputPort outputPort) : IRequestHandler<RentVehicleCommand, ICommandResult>
    {
        /// <summary>
        /// Handles the RentVehicleCommand by converting the command data to domain objects and executing the corresponding use case.
        /// </summary>
        /// <param name="command">The command containing the rental information.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task<ICommandResult> Handle(RentVehicleCommand command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command);

            var input = new RentVehicleInput(
                new LicensePlate(command.LicensePlate),
                new PersonalIdentificationNumber(command.CustomerIdNumber),
                new Period(command.StartDate, command.EndDate));

            await useCase.Execute(input);

            return (ICommandResult)outputPort;
        }
    }
}
