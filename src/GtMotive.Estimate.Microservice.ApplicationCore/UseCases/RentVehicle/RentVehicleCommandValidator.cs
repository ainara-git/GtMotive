using System;
using FluentValidation;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Validates the properties of the <see cref="RentVehicleCommand"/>.
    /// </summary>
    public class RentVehicleCommandValidator : AbstractValidator<RentVehicleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleCommandValidator"/> class.
        /// </summary>
        public RentVehicleCommandValidator()
        {
            RuleFor(x => x.LicensePlate).NotEmpty();
            RuleFor(x => x.CustomerIdNumber).NotEmpty();

            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("Start date is required")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Start date cannot be in the past");

            RuleFor(x => x.EndDate)
                .NotNull().WithMessage("End date is required")
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be on or after start date")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("End date must be today or later");
        }
    }
}
