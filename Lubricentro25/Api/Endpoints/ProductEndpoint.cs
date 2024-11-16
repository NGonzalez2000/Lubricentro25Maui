using Lubricentro25.Api.Contracts.Product;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public class ProductEndpoint(ILubricentroApiClient apiClient) : IProductEndpoint
{
    public async Task<ApiResponse<Product>> Create(Product product)
    {
        CreateProductRequest request = new(product);
        return await apiClient.Post<Product, ProductResponse>("Product/Create", request);
    }

    public async Task<ApiResponse> Delete(Product product)
    {
        DeleteProductRequest request = new(product.Id);
        return await apiClient.Delete("Product/Delete", request);
    }

    public async Task<ApiResponse<Product>> GetAllAsync()
    {
        return await apiClient.Get<Product, ProductResponse>("Product/GetAll");
    }

    public async Task<ApiResponse<Product>> Update(Product product)
    {
        UpdateProductRequest request = new(product);
        return await apiClient.Post<Product, ProductResponse>("Product/Update", request);
    }
}
