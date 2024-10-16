using Lubricentro25.Api.Contracts.Brand;

namespace Lubricentro25.Api.Contracts.Product;

public record ProductResponse(string Id,
                          string Code,
                          string Barcode,
                          string Description,
                          string ProviderId,
                          string ProviderName,
                          BrandResponse Brand,
                          decimal ListPrice,
                          decimal SellPrice,
                          decimal MarkupPercentage,
                          bool IsWholesaler,
                          bool IsUsd)
{
}
