using Lubricentro25.Models.Helpers;

namespace Lubricentro25.Api.Interface;

public interface IAuthenticationEndpoint
{
    Task<ApiResponse<AuthenticationHelper>> PolicyValidation(string policyName);
}
