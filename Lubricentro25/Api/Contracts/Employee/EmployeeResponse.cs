﻿namespace Lubricentro25.Api.Contracts.Employee;

public record EmployeeResponse(
    byte[]? Image,
    string Id,
    string FirstName,
    string LastName,
    string Cuil,
    string Email,
    string RoleId,
    string RoleName
    );