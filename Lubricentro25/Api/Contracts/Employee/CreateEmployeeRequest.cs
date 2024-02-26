namespace Lubricentro25.Api.Contracts.Employee;

public record CreateEmployeeRequest(string RoleId, string FirstName, string LastName, string Email)
{
}
