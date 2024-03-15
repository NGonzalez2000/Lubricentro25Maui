using Lubricentro25.Api.Contracts.Login;

namespace Lubricentro25.Api.Interface;

public interface ILoginEndpoint
{
    Task<ApiResponse> Login(LoginRequest request);
}
