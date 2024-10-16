using Lubricentro25.Api.Contracts.Brand;
using Mapster;

namespace Lubricentro25.Models.Mapping;

internal class BrandMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Brand, BrandResponse>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.Providers, src => src.Providers);
    }
}
