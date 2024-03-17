using Lubricentro25.Api.Contracts.Roles;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public class RoleEndpoint(ILubricentroApiClient apiClient) : IRoleEndpoint
{
    private readonly ILubricentroApiClient _apiClient = apiClient;
    public async Task<ApiResponse<Role>> CreateRole(Role role)
    {
        List<string> policiesId = [];
        foreach (var policy in role.Policies)
        {
            policiesId.Add(policy.Id);
        }
        var request = new CreateRoleRequest(role.Name, policiesId);

        return await _apiClient.Post<Role,RoleResponse>("/role/create", request);   

    }

    public async Task<ApiResponse> DeleteRole(string Id)
    {
        var request = new DeleteRoleRequest(Id);
        return await _apiClient.Delete("/role",request);
    }

    public async Task<ApiResponse<Role>> GetAllRoles()
    {
        return await _apiClient.Get<Role,RoleResponse>("/role/getall");
    }

    public Task<ApiResponse<Role>> UpdateRole(Role role)
    {
        List<string> policiesId = [];
        foreach (var policy in role.Policies)
        {
            policiesId.Add(policy.Id);
        }
        var request = new UpdateRoleRequest(role.Id, role.Name, policiesId);
        return _apiClient.Post<Role, RoleResponse>("/role/update", request);
    }

    public async Task<ApiResponse<Policy>> GetAllPolicies()
    {
        return await _apiClient.Get<Policy, PolicyResponse>("role/getallpolicies");
    }
}
