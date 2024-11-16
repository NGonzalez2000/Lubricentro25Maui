namespace Lubricentro25.Api.Interface;

public interface IDiscountEndpoint
{
    Task<ApiResponse<Discount>> Create(Discount discount, Brand brand);
    Task<ApiResponse<Discount>> Update(Discount discount);
    Task<ApiResponse> Delete(Discount discount, Brand brand);
    Task<ApiResponse<List<Discount>>> GetBrandDiscounts(Brand brand);

}
