namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases
{
    /// <summary>
    /// Interface for handling general errors in use cases.
    /// </summary>
    public interface IOutputPortError
    {
        /// <summary>
        /// Handles general errors that occur during use case execution.
        /// </summary>
        /// <param name="message">Error message describing what went wrong.</param>
        void ErrorHandle(string message);
    }
}
