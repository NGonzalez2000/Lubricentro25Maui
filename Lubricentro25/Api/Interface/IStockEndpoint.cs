using Lubricentro25.Models.Stock;

namespace Lubricentro25.Api.Interface;

public interface IStockEndpoint
{
    Task<ApiResponse<Stock>> GetStock();
}
