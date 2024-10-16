using Lubricentro25.Api.Contracts.Product;
using Mapster;

namespace Lubricentro25.Models.Mapping;

internal class ProductMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProductResponse, Product>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.Provider.Id, src => src.ProviderId)
            .Map(dest => dest.Provider.Name, src => src.ProviderName);
    }
}
