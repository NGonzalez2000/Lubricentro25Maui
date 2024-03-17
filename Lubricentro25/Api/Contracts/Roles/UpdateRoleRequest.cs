namespace Lubricentro25.Api.Contracts.Roles;
public record UpdateRoleRequest(string Id, string Name, List<string> PolicyIds);
