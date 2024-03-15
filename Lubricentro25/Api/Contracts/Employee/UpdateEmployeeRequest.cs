namespace Lubricentro25.Api.Contracts.Employee;

public record UpdateEmployeeRequest(string Id, string RoleId, string FirstName, string LastName)
{
}
