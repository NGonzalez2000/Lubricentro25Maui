using Lubricentro25.Api.Contracts.TaxCondition;
using Lubricentro25.ViewModels.Configurations.Migrations;
using Mapster;

namespace Lubricentro25.Models.Mapping;

class TaxConditionMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TaxConditionResponse, TaxCondition>()
            .Map(dest => dest.Vat, src => src.VAT)
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest, src => src);
    }
}
