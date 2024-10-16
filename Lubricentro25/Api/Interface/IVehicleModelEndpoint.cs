using Lubricentro25.Models.Vehicles;

namespace Lubricentro25.Api.Interface;

public interface IVehicleModelEndpoint
{
    Task<ApiResponse<VehicleModel>> GetALlAsync();
    Task<ApiResponse<VehicleModel>> Create(VehicleFactory vehicleFactory,VehicleModel vehicleModel);
    Task<ApiResponse<VehicleModel>> Update(VehicleModel vehicleModel);
    Task<ApiResponse> Delete(VehicleModel vehicleModel);
}
