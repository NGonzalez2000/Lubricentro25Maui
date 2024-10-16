namespace Lubricentro25.Api.Contracts.Companies;

internal record UpdateCompanyRequest(string Id, string Name, string Cuil, string Email, string ClientId, string ClientSecret)
{
}
