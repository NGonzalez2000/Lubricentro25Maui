namespace Lubricentro25.Api.Contracts.Migrations;

public record CreateTaxConditionMigrationRequest(string OldId, string Description, char Type, bool Vat)
{
}
