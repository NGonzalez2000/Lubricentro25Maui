namespace Lubricentro25.Api.Contracts.VatTypes;

public record VatTypeResponse(string Id, string Description, decimal Aliquota, int AfipCode)
{
}
