using Lubricentro25.Api.Contracts.Stock;
using Mapster;
using Lubricentro25.Models.Stock;

namespace Lubricentro25.Models.Mapping;

internal class StockMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<StockItemLocationResponse, StockLocation>()
            .Map(dest => dest, src => src);
            //.Map(dest => dest.X, src => src.X)
            //.Map(dest => dest.Y, src => src.Y)
            //.Map(dest => dest.Z, src => src.Z);
        config.NewConfig<StockItemResponse, StockItem>()
            .Map(dest => dest, src => src);
        config.NewConfig<StockResponse, Stock.Stock>()
            .Map(dest => dest, src => src);
    }
}
