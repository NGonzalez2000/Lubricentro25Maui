using Lubricentro25.Models.Vehicles;

namespace Lubricentro25.Api.Interface
{
    public interface IVehicleFactoryEndpoint
    {
        Task<ApiResponse<VehicleFactory>> GetALlAsync();
        Task<ApiResponse<VehicleFactory>> Create(VehicleFactory vehicleFactory);
        Task<ApiResponse<VehicleFactory>> Update(VehicleFactory vehicleFactory);
        Task<ApiResponse> Delete(VehicleFactory vehicleFactory);
    }
}
