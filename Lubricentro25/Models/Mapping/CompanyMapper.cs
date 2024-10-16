using Lubricentro25.Api.Contracts.Companies;
using Mapster;

namespace Lubricentro25.Models.Mapping;

internal class CompanyMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CompanyResponse, Company>()
            .Map(dest => dest, src => src);
    }
}
