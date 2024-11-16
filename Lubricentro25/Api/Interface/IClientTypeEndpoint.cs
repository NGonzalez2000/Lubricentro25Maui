namespace Lubricentro25.Api.Interface;

public interface IClientTypeEndpoint
{
    Task<ApiResponse<ClientType>> GetAllAsync();
    Task<ApiResponse<ClientType>> Create(ClientType clientType);
    Task<ApiResponse<ClientType>> Update(ClientType clientType);
    Task<ApiResponse> Delete(ClientType clientType);
}
