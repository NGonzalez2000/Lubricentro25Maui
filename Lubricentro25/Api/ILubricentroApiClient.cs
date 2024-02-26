using Lubricentro25.Api.Contracts.Login;

namespace Lubricentro25.Api
{
    public interface ILubricentroApiClient
    {
        // AUTHORIZATION //
        Task<LoginResponse> Login(LoginRequest request);

        // ROLE //
        Task<List<Role>?> GetAllRoles();
        Task<Role> CreateRole(Role role);
        Task<Role> UpdateRole(Role role);
        Task<Role> DeleteRole(string Id);

        // EMPLOYEE //
        Task<List<Employee>?> GetAllEmployees();
        Task<Employee?> CreateEmployee(Employee employee);
        Task<Employee?> UpdateEmployee(Employee employee);
        Task<Employee?> DeleteEmployee(string id);
    }
}
