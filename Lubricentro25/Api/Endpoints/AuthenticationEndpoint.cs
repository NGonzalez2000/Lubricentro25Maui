using Lubricentro25.Api.Contracts.Login;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Helpers;

namespace Lubricentro25.Api.Endpoints;

public class AuthenticationEndpoint(ILubricentroApiClient apiClient) : IAuthenticationEndpoint
{
    private readonly ILubricentroApiClient _apiClient = apiClient;
    public Task<ApiResponse<AuthenticationHelper>> PolicyValidation(string policyName)
    {
        return _apiClient.Post<AuthenticationHelper,PolicyValidationResponse>("/auth/policyverification", new PolicyValidationRequest(policyName));
    }
}
