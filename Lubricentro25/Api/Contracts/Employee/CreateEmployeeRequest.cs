namespace Lubricentro25.Api.Contracts.Employee;

public record CreateEmployeeRequest(byte[]? ImageData,string RoleId, string FirstName, string LastName, string Email)
{
}
