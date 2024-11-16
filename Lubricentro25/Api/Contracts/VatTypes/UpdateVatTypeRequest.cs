namespace Lubricentro25.Api.Contracts.VatTypes;

public record UpdateVatTypeRequest(string Id, string Description, decimal Aliquita, int AfipCode)
{
}
