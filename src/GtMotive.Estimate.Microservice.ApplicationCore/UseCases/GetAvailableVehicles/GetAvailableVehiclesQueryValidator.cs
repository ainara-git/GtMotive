using FluentValidation;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Validates the properties of the <see cref="GetAvailableVehiclesQuery"/>.
    /// </summary>
    public class GetAvailableVehiclesQueryValidator : AbstractValidator<GetAvailableVehiclesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableVehiclesQueryValidator"/> class.
        /// </summary>
        public GetAvailableVehiclesQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .NotNull().WithMessage("Start date is required");

            RuleFor(x => x.EndDate)
                .NotNull().WithMessage("End date is required")
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be on or after start date");
        }
    }
}
