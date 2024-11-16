namespace Lubricentro25.Api.Contracts.ClientTypes;

public record UpdateClientTypeRequest(string Id, string Description, int Order)
{
}
