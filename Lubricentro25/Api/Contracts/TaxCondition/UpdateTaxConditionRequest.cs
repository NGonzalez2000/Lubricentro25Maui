namespace Lubricentro25.Api.Contracts.TaxCondition;

public record UpdateTaxConditionRequest(string Id,
                                        string Description,
                                        char Type,
                                        bool Vat)
{
}
