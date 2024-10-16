namespace Lubricentro25.Api.Contracts.Product;

public record CreateProductRequest(string Code,
                                   string Barcode,
                                   string Description,
                                   string ProviderId,
                                   string BrandId,
                                   decimal ListPrice,
                                   decimal SellPrice,
                                   decimal MarkupPercentage,
                                   bool IsWholesaler,
                                   bool IsUsd)
{
    public CreateProductRequest(Models.Product product) : this(product.Code, product.Barcode, product.Description, product.Brand.Providers[0].Id, product.Brand.Id, product.ListPrice, product.SellPrice, product.MarkupPercentage, product.IsWholesaler, product.IsUsd)
    {
        
    }
}
