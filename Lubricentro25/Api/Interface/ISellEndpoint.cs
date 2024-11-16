namespace Lubricentro25.Api.Interface;

public interface ISellEndpoint
{
    Task<ApiResponse<SellItem>> GetSellItemsAsync();
}
