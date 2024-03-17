namespace Lubricentro25.Api.Interface;

public interface IRoleEndpoint
{
    Task<ApiResponse<Role>> GetAllRoles();
    Task<ApiResponse<Role>> CreateRole(Role role);
    Task<ApiResponse<Role>> UpdateRole(Role role);
    Task<ApiResponse> DeleteRole(string Id);
    Task<ApiResponse<Policy>> GetAllPolicies();
}
