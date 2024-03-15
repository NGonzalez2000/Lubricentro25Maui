namespace Lubricentro25.Api.Contracts.Roles;

public record CreateRoleRequest(string Name, List<string> Policies)
{
}
