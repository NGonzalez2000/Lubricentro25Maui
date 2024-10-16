using Lubricentro25.Api.Contracts.Providers;

namespace Lubricentro25.Api.Interface;

public interface IProviderEndpoint
{
    Task<ApiResponse<Provider>> GetAll();
    Task<ApiResponse<Provider>> Create(Provider provider);
    Task<ApiResponse<Provider>> Update(Provider provider);
    Task<ApiResponse> Delete(Provider provider);
}
