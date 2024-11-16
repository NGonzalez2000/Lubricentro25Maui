using Lubricentro25.Api.Contracts.ClientTypes;
using Mapster;

namespace Lubricentro25.Models.Mapping;

public class ClientTypeMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ClientTypeResponse, ClientType>()
            .Map(dest => dest, src => src);
    }
}
