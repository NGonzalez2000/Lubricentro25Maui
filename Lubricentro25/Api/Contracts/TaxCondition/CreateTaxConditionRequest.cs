namespace Lubricentro25.Api.Contracts.TaxCondition;

public record CreateTaxConditionRequest(string Description, char Type, bool Vat)
{
}
