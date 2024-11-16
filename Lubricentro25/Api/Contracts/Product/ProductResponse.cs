using Lubricentro25.Api.Contracts.Brand;
using Lubricentro25.Api.Contracts.Discount;

namespace Lubricentro25.Api.Contracts.Product;

public record ProductResponse(string Id,
                              string Code,
                              string Barcode,
                              string Description,
                              BrandResponse Brand,
                              DiscountResponse Discount,
                              decimal ListPrice,
                              decimal SellPrice,
                              decimal MarkupPercentage,
                              bool IsWholesaler,
                              bool IsUsd,
                              VatType VatType,
                              decimal ItemsPerPackage,
                              decimal MinSellUnit,
                              DateTime LastUpdate)
{
}
