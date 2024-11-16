using Lubricentro25.Api.Contracts.Email;
using Lubricentro25.Api.Contracts.Phone;

namespace Lubricentro25.Api.Contracts.Customers;

public record UpdateClientRequest(string Id,
                                  string Country,
                                  string State,
                                  string City,
                                  string Street,
                                  string PostalCode,
                                  string ClientTypeId,
                                  string TaxConditionId,
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
