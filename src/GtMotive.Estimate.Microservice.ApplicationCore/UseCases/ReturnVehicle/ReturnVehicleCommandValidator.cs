using FluentValidation;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Validates the properties of the <see cref="ReturnVehicleCommand"/>.
    /// </summary>
    public class ReturnVehicleCommandValidator : AbstractValidator<ReturnVehicleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleCommandValidator"/> class.
        /// </summary>
        public ReturnVehicleCommandValidator()
        {
            RuleFor(x => x.LicensePlate).NotEmpty();
            RuleFor(x => x.CustomerIdNumber).NotEmpty();
        }
    }
}
