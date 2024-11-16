namespace Lubricentro25.Api.Contracts.Stock;

public record StockItemResponse(string Id,
                                decimal Quantity,
                                string Code,
                                string Barcode,
                                string Description,
                                string Brand,
                                int MinStock,
                                int MaxStock,
                                StockItemLocationResponse Location,
                                StockItemLocationResponse Deposit)
{
}