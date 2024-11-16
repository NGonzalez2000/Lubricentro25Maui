using Lubricentro25.Api.Contracts.Sell;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public class SellEndpoint(ILubricentroApiClient apiClient) : ISellEndpoint
{
    public Task<ApiResponse<SellItem>> GetSellItemsAsync()
    {
        return apiClient.Get<SellItem, SellItemResponse>("Sell/Items");
    }
}
