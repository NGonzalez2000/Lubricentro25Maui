using Lubricentro25.Api.Contracts.VehicleModel;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Vehicles;

namespace Lubricentro25.Api.Endpoints;

public class VehicleModelEndpoint(ILubricentroApiClient apiClient) : IVehicleModelEndpoint
{
    public async Task<ApiResponse<VehicleModel>> Create(VehicleFactory vehicleFactory, VehicleModel vehicleModel)
    {
        CreateVehicleModelRequest request = new(vehicleFactory.Id, vehicleModel.Name, vehicleModel.IsLight);
        return await apiClient.Post<VehicleModel, VehicleModelResponse>("Vehicle/Models/Create", request);
    }

    public async Task<ApiResponse> Delete(VehicleModel vehicleModel)
    {
        DeleteVehicleModelRequest request = new(vehicleModel.Id);
        return await apiClient.Delete("Vehicle/Models/Delete", request);
    }

    public async Task<ApiResponse<VehicleModel>> GetALlAsync()
    {
        return await apiClient.Get<VehicleModel, VehicleModelResponse>("Vehicle/Models/GetAll");
    }

    public async Task<ApiResponse<VehicleModel>> Update(VehicleModel vehicleModel)
    {
        UpdateVehicleModelRequest request = new(vehicleModel.Id, vehicleModel.Name, vehicleModel.IsLight);
        return await apiClient.Post<VehicleModel, VehicleModelResponse>("Vehicle/Models/Update", request);
    }
}
