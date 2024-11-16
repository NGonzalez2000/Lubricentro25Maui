using Lubricentro25.Api.Contracts.Stock;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Stock;

namespace Lubricentro25.Api.Endpoints;

public class StockEndpoint(ILubricentroApiClient apiClient) : IStockEndpoint
{
    public Task<ApiResponse<Stock>> GetStock()
    {
        return apiClient.Post<Stock, StockResponse>("Stock", new { });
    }
}
