namespace Lubricentro25.Api.Contracts.VehicleModel;

public record CreateVehicleModelRequest(string VehicleFactoryId, string Name, bool IsLight)
{
}
