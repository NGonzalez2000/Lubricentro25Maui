﻿using Lubricentro25.Api.Contracts.Login;

namespace Lubricentro25.Api.Interface;

public interface IEmployeeEndpoint
{
    Task<ApiResponse<Employee>> GetAllEmployees();
    Task<ApiResponse<Employee>> CreateEmployee(Employee employee);
    Task<ApiResponse<Employee>> UpdateEmployee(Employee employee);
    Task<ApiResponse> DeleteEmployee(string id);
    Task<ApiResponse<AuthenticationResponse>> ChangeEmployeePassword(string email, string password);
}
