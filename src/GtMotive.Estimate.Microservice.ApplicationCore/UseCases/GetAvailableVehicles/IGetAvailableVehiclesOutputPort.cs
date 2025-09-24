namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Output port interface for the list available vehicles use case.
    /// </summary>
    public interface IGetAvailableVehiclesOutputPort : IOutputPortStandard<GetAvailableVehiclesOutput>, IOutputPortError
    {
    }
}
