using Lubricentro25.Api.Contracts.Email;
using Lubricentro25.Api.Contracts.Phone;

namespace Lubricentro25.Api.Contracts.Providers;

public record UpdateProviderRequest(string Id,
                                    string Name,
                                    string Cuil,
                                    List<PhoneRequest> Phones,
                                    List<EmailRequest> Emails,
                                    string Fax,
                                    string Observation,
                                    string Website,
                                    string Country,
                                    string State,
                                    string City,
                                    string Street,
                                    string PostalCode,
                                    string TaxConditionId)
{
}
