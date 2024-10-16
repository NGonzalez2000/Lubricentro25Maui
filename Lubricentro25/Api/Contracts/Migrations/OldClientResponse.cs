using Lubricentro25.Api.Contracts.Email;
using Lubricentro25.Api.Contracts.Phone;

namespace Lubricentro25.Api.Contracts.Migrations;

public record OldClientResponse(string Id,
                                string AddressId,
                                string Country,
                                string State,
                                string City,
                                string Street,
                                string PostalCode,
                                string TaxConditionId,
                                string TaxConditionDescription,
                                char TaxConditionType,
                                bool TaxConditionVAT,
                                string ClientName,
                                string Cuil,
                                bool HasEmailNotification,
                                List<EmailResponse> Emails,
                                bool HasPhoneNotification,
                                List<PhoneResponse> Phones,
                                string Observation,
                                bool HasCheckingAccount,
                                bool IsWholesaler)
{
}
