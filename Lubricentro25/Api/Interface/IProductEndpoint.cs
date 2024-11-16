using Lubricentro25.Models;

namespace Lubricentro25.Api.Interface;

public interface IProductEndpoint
{
    Task<ApiResponse<Product>> GetAllAsync();
    Task<ApiResponse<Product>> Create(Product product);
    Task<ApiResponse<Product>> Update(Product product);
    Task<ApiResponse> Delete(Product product);
}
