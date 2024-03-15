using Lubricentro25.Api.Contracts.Employee;
using Lubricentro25.Api.Interface;

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
        var request = new CreateEmployeeRequest(employee.Role.Id, employee.FirstName, employee.LastName, employee.Email);
        return await _apiClient.Post<Employee,EmployeeResponse>("/employee/create", request);
    }

    public async Task<ApiResponse> DeleteEmployee(string id)
    {
        var request = new DeleteEmployeeRequest(id);
        return await _apiClient.Delete("/employee/delete", request);
    }


    public async Task<ApiResponse<Employee>> UpdateEmployee(Employee employee)
    {
        var request = new UpdateEmployeeRequest(employee.Id, employee.Role.Id, employee.FirstName, employee.LastName);
        return await _apiClient.Post<Employee, EmployeeResponse>("/employee/update", request);
    }
}
