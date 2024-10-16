namespace Lubricentro25.Api.Contracts.VehicleModel;

public record UpdateVehicleModelRequest(string Id, string Name, bool IsLight)
{
}
