using System;

namespace GtMotive.Estimate.Microservice.Domain.Common
{
    /// <summary>
    /// Provides testable DateTime operations by allowing time injection in tests.
    /// </summary>
    public static class DateTimeProvider
    {
        /// <summary>
        /// Gets or sets current UTC time provider. Can be mocked for testing.
        /// </summary>
        public static Func<DateTime> UtcNow { get; set; } = () => DateTime.UtcNow;

        /// <summary>
        /// Gets current UTC date without time. Testable via UtcNow override.
        /// </summary>
        public static DateTime Today => UtcNow().Date;
    }
}
