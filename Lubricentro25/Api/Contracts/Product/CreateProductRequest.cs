namespace Lubricentro25.Api.Contracts.Product;

public record CreateProductRequest(string Code,
                                   string Barcode,
                                   string Description,
                                   string BrandId,
                                   string DiscountId,
                                   decimal ListPrice,
                                   decimal SellPrice,
                                   decimal MarkupPercentage,
                                   bool IsWholesaler,
                                   bool IsUsd,
                                   VatType VatType,
                                   decimal ItemsPerPackage,
                                   decimal MinSellUnit)
{
    public CreateProductRequest(Models.Product product) : this(product.Code, product.Barcode, product.Description, product.Brand.Id, product.Discount.Id, product.ListPrice, product.SellPrice, product.MarkupPercentage, product.IsWholesaler, product.IsUsd, product.VatType, product.ItemsPerPackage, product.MinSellUnit)
    {
        
    }
}
