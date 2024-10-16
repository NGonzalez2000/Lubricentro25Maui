using Lubricentro25.Api.Contracts.Email;
using Lubricentro25.Api.Contracts.Phone;

namespace Lubricentro25.Api.Contracts.Customers;

public record CreateClientRequest(string Country,
                                  string State,
                                  string City,
                                  string Street,
                                  string PostalCode,
                                  string ClientName,
                                  string TaxConditionId,
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
