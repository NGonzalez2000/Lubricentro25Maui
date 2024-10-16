using Lubricentro25.Api.Contracts.Employee;
using Lubricentro25.Api.Contracts.Login;
using Lubricentro25.Api.Interface;
using Lubricentro25.Services;

namespace Lubricentro25.Api.Endpoints;

public class EmployeeEndpoint(ILubricentroApiClient apiClient) : IEmployeeEndpoint
{
    private readonly ILubricentroApiClient _apiClient = apiClient;
    public async Task<ApiResponse<Employee>> GetAllEmployees()
    {
        return await _apiClient.Get<Employee,EmployeeResponse>("/employee/getall");
    }
    public async Task<ApiResponse<Employee>> CreateEmployee(Employee employee)
    {
        var request = new CreateEmployeeRequest(await ImageParser.ImageSourceToBytes(employee.ImageSource),employee.Role.Id, employee.FirstName, employee.LastName, employee.Email);
        return await _apiClient.Post<Employee,EmployeeResponse>("/employee/create", request);
    }

    public async Task<ApiResponse> DeleteEmployee(string id)
    {
        var request = new DeleteEmployeeRequest(id);
        return await _apiClient.Delete("/employee/delete", request);
    }


    public async Task<ApiResponse<Employee>> UpdateEmployee(Employee employee)
    {
        var request = new UpdateEmployeeRequest(await ImageParser.ImageSourceToBytes(employee.ImageSource), employee.Id, employee.Role.Id, employee.FirstName, employee.LastName, employee.Cuil);
        return await _apiClient.Post<Employee, EmployeeResponse>("/employee/update", request);
    }

    public async Task<ApiResponse<AuthenticationResponse>> ChangeEmployeePassword(string email, string password)
    {
        var request = new ChangeEmployeePasswordRequest(email, password);
        return await _apiClient.Post<AuthenticationResponse, AuthenticationResponse>("/auth/PasswordChange", request);
    }
}
