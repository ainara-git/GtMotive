using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Handles the execution of ReturnVehicleCommand.
    /// </summary>
    public sealed class ReturnVehicleCommandHandler(ReturnVehicleUseCase useCase, IReturnVehicleOutputPort outputPort) : IRequestHandler<ReturnVehicleCommand, ICommandResult>
    {
        /// <summary>
        /// Handles the ReturnVehicleCommand by converting the command data to domain objects and executing the corresponding use case.
        /// </summary>
        /// <param name="command">The command containing the return vehicle information.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation containing the command result.</returns>
        public async Task<ICommandResult> Handle(ReturnVehicleCommand command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command);

            var input = new ReturnVehicleInput(
                new LicensePlate(command.LicensePlate),
                new PersonalIdentificationNumber(command.CustomerIdNumber));

            await useCase.Execute(input);

            return (ICommandResult)outputPort;
        }
    }
}
