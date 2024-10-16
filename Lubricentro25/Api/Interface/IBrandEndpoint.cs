namespace Lubricentro25.Api.Interface;

public interface IBrandEndpoint
{
    Task<ApiResponse<Brand>> GetALlAsync();
    Task<ApiResponse<Brand>> Create(Brand brand);
    Task<ApiResponse<Brand>> Update(Brand brand);
    Task<ApiResponse> Delete(string Id);
}
