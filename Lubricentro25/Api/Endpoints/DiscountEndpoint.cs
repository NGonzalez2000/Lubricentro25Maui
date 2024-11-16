using Lubricentro25.Api.Contracts.Discount;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public class DiscountEndpoint(ILubricentroApiClient apiClient) : IDiscountEndpoint
{
    public Task<ApiResponse<Discount>> Create(Discount discount, Brand brand)
    {
        CreateDiscountRequest request = new(brand, discount);
        return apiClient.Post<Discount,DiscountResponse>("Discount/Create", request);
    }

    public Task<ApiResponse> Delete(Discount discount, Brand brand)
    {
        DeleteDiscountRequest request = new(discount.Id, brand.Id);
        return apiClient.Delete("Discount/Delete", request);
    }

    public Task<ApiResponse<List<Discount>>> GetBrandDiscounts(Brand brand)
    {
        GetBrandDiscountsRequest request = new(brand.Id);
        return apiClient.Post<List<Discount>, List<DiscountResponse>>("Brand/GetDiscounts", request);
    }

    public Task<ApiResponse<Discount>> Update(Discount discount)
    {
        UpdateDiscountRequest request = new(discount);
        return apiClient.Post<Discount, DiscountResponse>("Discount/Update", request);
    }
}
