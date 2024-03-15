namespace Lubricentro25.Api.Contracts.Roles;

public record RoleResponse(string Id, string Name, List<PolicyResponse> Policies)
{
}

public record PolicyResponse(string Id, string Name);
