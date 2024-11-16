using Lubricentro25.Api.Contracts.Discount;
using Lubricentro25.Api.Contracts.VatTypes;

namespace Lubricentro25.Api.Contracts.Sell;

public record SellItemResponse(string StockItemId,
                               string ProductId,
                               string Code,
                               string Barcode,
                               string Description,
                               string BrandName,
                               DiscountResponse Discount,
                               VatTypeResponse VatType,
                               decimal ListPrice,
                               decimal MarkupPercentage,
                               decimal ItemsPerPackage,
                               decimal MinSellUnit,
                               bool IsWholesaler,
                               bool IsUsd,
                               decimal Quantity)
{
}
