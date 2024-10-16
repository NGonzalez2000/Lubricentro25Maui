using Lubricentro25.Api.Contracts.Email;
using Mapster;

namespace Lubricentro25.Models.Mapping;

public class EmailMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Email, EmailRequest>()
            .Map(dest => dest, src => src);
    }
}
