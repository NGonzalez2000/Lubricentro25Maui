namespace Lubricentro25.Api.Contracts.TaxCondition;

public record TaxConditionResponse(string Id,
                                   string Description,
                                   string Type,
                                   bool VAT)
{
}
