namespace Lubricentro25.Api.Contracts.Stock;

public record StockResponse(string Id, List<StockItemResponse> Items)
{
}
