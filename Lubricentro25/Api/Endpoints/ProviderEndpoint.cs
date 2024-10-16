using Lubricentro25.Api.Contracts.Email;
using Lubricentro25.Api.Contracts.Phone;
using Lubricentro25.Api.Contracts.Providers;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public class ProviderEndpoint(ILubricentroApiClient apiClient) : IProviderEndpoint
{
    public async Task<ApiResponse<Provider>> Create(Provider provider)
    {
        List<EmailRequest> emails = [];
        List<PhoneRequest> phones = [];
        foreach (var email in provider.EmailCollection.Emails) emails.Add(new(email.Id, email.Value, email.IsActive));
        foreach (var phone in provider.PhoneCollection.Phones) phones.Add(new(phone.Id, phone.NationalId, phone.Value, phone.IsActive));
        var request = new CreateProviderRequest(provider.Name,
                                                provider.Cuil,
                                                phones,
                                                emails,
                                                provider.Fax,
                                                provider.Observation,
                                                provider.Website,
                                                provider.Address.Country,
                                                provider.Address.State,
                                                provider.Address.City,
                                                provider.Address.Street,
                                                provider.Address.PostalCode,
                                                provider.TaxCondition.Id);
        return await apiClient.Post<Provider, ProviderResponse>("Provider/Create",request);
    }

    public async Task<ApiResponse> Delete(Provider provider)
    {
        var request = new DeleteProviderRequest(provider.Id);
        return await apiClient.Delete("Provider/Delete", request);
    }

    public async Task<ApiResponse<Provider>> GetAll()
    {
        return await apiClient.Get<Provider, ProviderResponse>("Provider/GetAll");
    }

    public async Task<ApiResponse<Provider>> Update(Provider provider)
    {
        List<EmailRequest> emails = [];
        List<PhoneRequest> phones = [];
        foreach (var email in provider.EmailCollection.Emails) emails.Add(new(email.Id, email.Value, email.IsActive));
        foreach (var phone in provider.PhoneCollection.Phones) phones.Add(new(phone.Id, phone.NationalId, phone.Value, phone.IsActive));
        var request = new UpdateProviderRequest(provider.Id,
                                                provider.Name,
                                                provider.Cuil,
                                                phones,
                                                emails,
                                                provider.Fax,
                                                provider.Observation,
                                                provider.Website,
                                                provider.Address.Country,
                                                provider.Address.State,
                                                provider.Address.City,
                                                provider.Address.Street,
                                                provider.Address.PostalCode,
                                                provider.TaxCondition.Id);
        return await apiClient.Post<Provider, ProviderResponse>("Provider/Update", request);
    }
}
