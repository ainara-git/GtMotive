namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output port for the return vehicle use case.
    /// Defines all possible outcomes when returning a rented vehicle.
    /// </summary>
    public interface IReturnVehicleOutputPort : IOutputPortStandard<ReturnVehicleOutput>, IOutputPortError, IOutputPortNotFound
    {
    }
}
