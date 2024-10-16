namespace Lubricentro25.Api.Contracts.Product;

public record UpdateProductRequest(string Id,
                                   string Code,
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
    public UpdateProductRequest(Models.Product product) : this(product.Id, product.Code, product.Barcode, product.Description, product.Provider.Id, product.Brand.Id, product.ListPrice, product.SellPrice, product.MarkupPercentage, product.IsWholesaler, product.IsUsd)
    {

    }
}
