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

        return await _apiClient.Post<Role,RoleResponse>("role/create", request);   

    }

    public Task<ApiResponse<Role>> DeleteRole(string Id)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<Role>> GetAllRoles()
    {
        return await _apiClient.Get<Role,RoleResponse>("/role/getall");
    }

    public Task<ApiResponse<Role>> UpdateRole(Role role)
    {
        throw new NotImplementedException();
    }
}
