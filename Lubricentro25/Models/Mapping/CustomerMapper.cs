using Lubricentro25.Api.Contracts.Customers;
using Lubricentro25.Api.Contracts.Migrations;
using Mapster;

namespace Lubricentro25.Models.Mapping;

class CustomerMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ClientResponse, Client>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.EmailCollection.HasEmailNotificationEnable, src => src.HasEmailNotification)
            .Map(dest => dest.EmailCollection.Emails, src => src.Emails)
            .Map(dest => dest.PhoneCollection.HasPhoneNotificationEnable, src => src.HasPhoneNotification)
            .Map(dest => dest.PhoneCollection.Phones, src => src.Phones)
            .Map(dest => dest.Address.Id, src => src.AddressId)
            .Map(dest => dest.Address.Country, src => src.Country)
            .Map(dest => dest.Address.State, src => src.State)
            .Map(dest => dest.Address.City, src => src.City)
            .Map(dest => dest.Address.Street, src => src.Street)
            .Map(dest => dest.Address.PostalCode, src => src.PostalCode);

        config.NewConfig<OldClientResponse, Client>()
            .Map(dest => dest, src => src)
            .Map(dest => dest.EmailCollection.HasEmailNotificationEnable, src => src.HasEmailNotification)
            .Map(dest => dest.EmailCollection.Emails, src => src.Emails)
            .Map(dest => dest.TaxCondition.Id, src => src.TaxConditionId)
            .Map(dest => dest.TaxCondition.Description, src => src.TaxConditionDescription)
            .Map(dest => dest.TaxCondition.Type, src => src.TaxConditionType)
            .Map(dest => dest.TaxCondition.Vat, src => src.TaxConditionVAT)
            .Map(dest => dest.Address.Id, src => src.AddressId)
            .Map(dest => dest.Address.Country, src => src.Country)
            .Map(dest => dest.Address.State, src => src.State)
            .Map(dest => dest.Address.City, src => src.City)
            .Map(dest => dest.Address.Street, src => src.Street)
            .Map(dest => dest.Address.PostalCode, src => src.PostalCode);
    }
}
