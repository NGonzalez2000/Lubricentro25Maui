using Lubricentro25.Api.Contracts.VehicleFactory;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Vehicles;

namespace Lubricentro25.Api.Endpoints;

public class VehicleFactoryEndpoint(ILubricentroApiClient apiClient) : IVehicleFactoryEndpoint
{
    public async Task<ApiResponse<VehicleFactory>> Create(VehicleFactory vehicleFactory)
    {
        CreateVehicleFactoryRequest request = new(vehicleFactory.Name);
        return await apiClient.Post<VehicleFactory, VehicleFactoryResponse>("Vehicle/Factories/Create", request);
    }

    public async Task<ApiResponse> Delete(VehicleFactory vehicleFactory)
    {
        DeleteVehicleFactoryRequest request = new(vehicleFactory.Id);
        return await apiClient.Delete("Vehicle/Factories/Delete", request);
    }

    public async Task<ApiResponse<VehicleFactory>> GetALlAsync()
    {
        return await apiClient.Get<VehicleFactory, VehicleFactoryResponse>("Vehicle/Factories/GetAll");
    }

    public async Task<ApiResponse<VehicleFactory>> Update(VehicleFactory vehicleFactory)
    {
        UpdateVehicleFactoryRequest request = new(vehicleFactory.Id, vehicleFactory.Name);
        return await apiClient.Post<VehicleFactory, VehicleFactoryResponse>("Vehicle/Factories/Update", request);
    }
}
