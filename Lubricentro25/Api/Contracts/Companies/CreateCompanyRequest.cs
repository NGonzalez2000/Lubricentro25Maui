namespace Lubricentro25.Api.Contracts.Companies;

public record CreateCompanyRequest(string Name, string Cuil, string Email, string ClientId, string ClientSecret)
{
}
