using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Behaviours
{
    /// <summary>
    /// MediatR behavior that validates requests using FluentValidation.
    /// Throws <see cref="ValidationException"/> if validation fails.
    /// </summary>
    /// <typeparam name="TRequest">The request type to validate.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
    {
        /// <summary>
        /// Validates the request and throws <see cref="ValidationException"/> if validation fails.
        /// </summary>
        /// <param name="request">The request to validate.</param>
        /// <param name="next">The next handler in the pipeline.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The response from the next handler if validation succeeds.</returns>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(next);

            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var failures = validators
                    .Select(v => v.Validate(context))
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
