namespace Lubricentro25.Api.Contracts.Branch;

public record BranchResponse(string Id, string Name, string PointOfSale, string Country, string State, string City, string Street, string PostalCode)
{
}
