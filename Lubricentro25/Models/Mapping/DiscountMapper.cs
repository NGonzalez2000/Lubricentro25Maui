using Lubricentro25.Api.Contracts.ClientTypeDiscount;
using Lubricentro25.Api.Contracts.Discount;
using Mapster;

namespace Lubricentro25.Models.Mapping;

internal class DiscountMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<DiscountResponse, Discount>()
            .Map(dest => dest.ClientTypeDiscounts, src => src.ClientTypeDiscounts)
            .Map(dest => dest, src => src);

        config.NewConfig<ClientTypeDiscountResponse, ClientTypeDiscount>()
            .Map(dest => dest, src => src);
    }
}
