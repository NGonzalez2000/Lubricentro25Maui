using Lubricentro25.Api.Contracts.VehicleModel;

namespace Lubricentro25.Api.Contracts.VehicleFactory;

public record VehicleFactoryResponse(string Id, string Name, List<VehicleModelResponse> Models)
{
}
