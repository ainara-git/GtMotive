namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle
{
    /// <summary>
    /// Output port for the add vehicle use case.
    /// </summary>
    public interface IAddVehicleOutputPort : IOutputPortStandard<AddVehicleOutput>, IOutputPortError
    {
    }
}
