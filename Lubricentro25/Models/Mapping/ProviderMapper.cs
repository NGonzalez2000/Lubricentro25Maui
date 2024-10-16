using Lubricentro25.Api.Contracts.Providers;
using Mapster;

namespace Lubricentro25.Models.Mapping;

internal class ProviderMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProviderResponse, Provider>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.EmailCollection.HasEmailNotificationEnable, src => true)
            .Map(dest => dest.EmailCollection.Emails, src => src.Emails)
            .Map(dest => dest.PhoneCollection.HasPhoneNotificationEnable, src => true)
            .Map(dest => dest.PhoneCollection.Phones, src => src.Phones)
            .Map(dest => dest.Address, src => src.Address);
    }
}
