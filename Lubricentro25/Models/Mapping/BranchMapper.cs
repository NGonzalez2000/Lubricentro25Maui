using Lubricentro25.Api.Contracts.Branch;
using Mapster;

namespace Lubricentro25.Models.Mapping;

internal class BranchMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BranchResponse, Branch>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.Address.Country, src => src.Country)
            .Map(dest => dest.Address.State, src => src.State)
            .Map(dest => dest.Address.City, src => src.City)
            .Map(dest => dest.Address.Street, src => src.Street)
            .Map(dest => dest.Address.PostalCode, src => src.PostalCode);
    }
}
