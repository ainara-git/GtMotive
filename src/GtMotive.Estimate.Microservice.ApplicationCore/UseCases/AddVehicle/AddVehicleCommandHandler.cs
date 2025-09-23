using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Common;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle
{
    /// <summary>
    /// Handles the execution of AddVehicleCommand.
    /// </summary>
    public sealed class AddVehicleCommandHandler(AddVehicleUseCase useCase, IAddVehicleOutputPort outputPort) : IRequestHandler<AddVehicleCommand, ICommandResult>
    {
        /// <summary>
        /// Handles the AddVehicleCommand by converting the command data to domain objects and executing the corresponding use case.
        /// </summary>
        /// <param name="command">The command containing the vehicle information to be added to the fleet.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>
        /// The task result containing the command result with the presentation-formatted response populated by the use case.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when the command is null.</exception>
        public async Task<ICommandResult> Handle(AddVehicleCommand command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command);

            var input = new AddVehicleInput(
                command.MakeAndModel,
                new LicensePlate(command.LicensePlate),
                command.ManufacturingDate);

            await useCase.Execute(input);

            return (ICommandResult)outputPort;
        }
    }
}
