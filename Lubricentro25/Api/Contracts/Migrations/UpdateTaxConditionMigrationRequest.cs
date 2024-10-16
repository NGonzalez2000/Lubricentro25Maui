namespace Lubricentro25.Api.Contracts.Migrations;

public record UpdateTaxConditionMigrationRequest(string Id, string Description, char Type, bool Vat)
{
}
