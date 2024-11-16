using Lubricentro25.Api.Contracts.Product;
using Mapster;

namespace Lubricentro25.Models.Mapping;

internal class ProductMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProductResponse, Product>()
            .Map(dest => dest.Discount, src => src.Discount)
            .Map(dest => dest, src => src);
    }
}
