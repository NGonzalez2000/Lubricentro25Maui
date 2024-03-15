using Lubricentro25.Api.Contracts.Login;

namespace Lubricentro25.Api;

public interface ILubricentroApiClient
{
    Task<ApiResponse<T>> Post<T,U>(string endPoint, object request);
    Task<ApiResponse<T>> Get<T,U>(string endPoint);
    Task<ApiResponse> Delete(string endPoint, object request);
    Task<ApiResponse> Login(LoginRequest request);
}
