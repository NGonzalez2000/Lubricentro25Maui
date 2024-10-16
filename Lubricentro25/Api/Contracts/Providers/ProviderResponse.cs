using Lubricentro25.Api.Contracts.Address;
using Lubricentro25.Api.Contracts.Email;
using Lubricentro25.Api.Contracts.Phone;
using Lubricentro25.Api.Contracts.TaxCondition;

namespace Lubricentro25.Api.Contracts.Providers;

public record ProviderResponse(string Id,
                               string Name,
                               string Cuil,
                               List<PhoneResponse> Phones,
                               List<EmailResponse> Emails,
                               string Fax,
                               string Observation,
                               string Website,
                               AddressResponse Address,
                               TaxConditionResponse TaxCondition)
{
}
