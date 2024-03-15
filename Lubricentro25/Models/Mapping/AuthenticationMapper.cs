using Lubricentro25.Api.Contracts.Login;
using Lubricentro25.Models.Helpers;
using Mapster;

namespace Lubricentro25.Models.Mapping
{
    internal class AuthenticationMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PolicyValidationResponse, AuthenticationHelper>()
                .Map(dest => dest.IsAllowed, src => src.IsValid);
        }
    }
}
