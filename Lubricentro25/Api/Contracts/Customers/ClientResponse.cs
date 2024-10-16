using Lubricentro25.Api.Contracts.Email;
using Lubricentro25.Api.Contracts.Phone;
using Lubricentro25.Api.Contracts.TaxCondition;

namespace Lubricentro25.Api.Contracts.Customers
{
    public record ClientResponse(string Id,
                                 string AddressId,
                                 string Country,
                                 string State,
                                 string City,
                                 string Street,
                                 string PostalCode,
                                 TaxConditionResponse TaxCondition,
                                 string ClientName,
                                 string Cuil,
                                 bool HasEmailNotification,
                                 List<EmailRequest> Emails,
                                 bool HasPhoneNotification,
                                 List<PhoneRequest> Phones,
                                 string Observation,
                                 bool HasCheckingAccount,
                                 bool IsWholesaler)
    {
    }
}
