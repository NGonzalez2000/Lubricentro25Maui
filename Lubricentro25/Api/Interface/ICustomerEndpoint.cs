namespace Lubricentro25.Api.Interface;

public interface ICustomerEndpoint
{
    Task<ApiResponse<Client>> GetAll();
    Task<ApiResponse<Client>> Create(Client client);
    Task<ApiResponse<Client>> Update(Client client);
    Task<ApiResponse> Delete(Client client);
}
