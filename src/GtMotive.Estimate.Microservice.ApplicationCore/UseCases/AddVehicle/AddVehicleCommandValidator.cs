using System;
using FluentValidation;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle
{
    /// <summary>
    /// Validates the properties of the <see cref="AddVehicleCommand"/>.
    /// </summary>
    public class AddVehicleCommandValidator : AbstractValidator<AddVehicleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddVehicleCommandValidator"/> class.
        /// </summary>
        public AddVehicleCommandValidator()
        {
            RuleFor(x => x.MakeAndModel)
                .NotEmpty().WithMessage("Make and model is required.");

            RuleFor(x => x.LicensePlate)
                .NotEmpty().WithMessage("License plate is required.");

            RuleFor(x => x.ManufacturingDate)
                .NotNull().WithMessage("Manufacturing date is required.")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Manufacturing date cannot be in the future.");
        }
    }
}
