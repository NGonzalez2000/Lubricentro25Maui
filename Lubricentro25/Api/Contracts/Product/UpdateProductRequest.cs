namespace Lubricentro25.Api.Contracts.Product;

public record UpdateProductRequest(string Id,
                                   string Code,
                                   string Barcode,
                                   string Description,
                                   string BrandId,
                                   string DiscountId,
                                   decimal ListPrice,
                                   decimal SellPrice,
                                   decimal MarkupPercentage,
                                   bool IsWholesaler,
                                   bool IsUsd,
                                   string VatTypeId,
                                   decimal ItemsPerPackage,
                                   decimal MinSellUnit)
{
    public UpdateProductRequest(Models.Product product) : this(product.Id, product.Code, product.Barcode, product.Description, product.Brand.Id, product.Discount.Id, product.ListPrice, product.SellPrice, product.MarkupPercentage, product.IsWholesaler, product.IsUsd, product.VatType.Id, product.ItemsPerPackage, product.MinSellUnit)
    {

    }
}
