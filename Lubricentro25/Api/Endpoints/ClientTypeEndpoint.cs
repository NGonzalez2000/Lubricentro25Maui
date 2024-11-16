using Lubricentro25.Api.Contracts.ClientTypes;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public class ClientTypeEndpoint(ILubricentroApiClient apiClient) : IClientTypeEndpoint
{
    public async Task<ApiResponse<ClientType>> Create(ClientType clientType)
    {
        CreateClientTypeRequest request = new(clientType.Description, clientType.Order);
        return await apiClient.Post<ClientType, ClientTypeResponse>("ClientType/Create", request);
    }

    public async Task<ApiResponse> Delete(ClientType clientType)
    {
        DeleteClientTypeRequest request = new(clientType.Id);
        return await apiClient.Delete("ClientType/Delete", request);
    }

    public async Task<ApiResponse<ClientType>> GetAllAsync()
    {
        return await apiClient.Get<ClientType, ClientTypeResponse>("ClientType/GetAll");
    }

    public async Task<ApiResponse<ClientType>> Update(ClientType clientType)
    {
        UpdateClientTypeRequest request = new(clientType.Id, clientType.Description, clientType.Order);
        return await apiClient.Post<ClientType, ClientTypeResponse>("ClientType/Update", request);
    }
}
