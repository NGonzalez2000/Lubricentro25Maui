namespace Lubricentro25.Api.Contracts.Employee;

public record UpdateEmployeeRequest(byte[]? ImageData, string Id, string RoleId, string FirstName, string LastName, string Cuil)
{
}
