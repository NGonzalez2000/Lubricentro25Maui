using Lubricentro25.Api.Contracts.Customers;
using Lubricentro25.Api.Contracts.Email;
using Lubricentro25.Api.Contracts.Phone;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

class CustomerEndpoint(ILubricentroApiClient _apiClient) : ICustomerEndpoint
{
    public Task<ApiResponse<Client>> Create(Client client)
    {
        List<EmailRequest> emails = [];
        List<PhoneRequest> phones = [];
        foreach (var email in client.EmailCollection.Emails) emails.Add(new(email.Id, email.Value, email.IsActive));
        foreach (var phone in client.PhoneCollection.Phones) phones.Add(new(phone.Id, phone.NationalId, phone.Value, phone.IsActive));

        CreateClientRequest request = new(client.Address.Country,
                                          client.Address.State,
                                          client.Address.City,
                                          client.Address.Street,
                                          client.Address.PostalCode,
                                          client.ClientName,
                                          client.ClientType.Id,
                                          client.TaxCondition.Id,
                                          client.Cuil,
                                          client.EmailCollection.HasEmailNotificationEnable,
                                          emails,
                                          client.PhoneCollection.HasPhoneNotificationEnable,
                                          phones,
                                          client.Observation,
                                          client.HasCheckingAccount,
                                          client.IsWholesaler);

        return _apiClient.Post<Client, ClientResponse>("Client/Create", request);
    }

    public async Task<ApiResponse> Delete(Client client)
    {
        DeleteClientRequest request = new(client.Id);
        return await _apiClient.Delete("Client/Delete", request);
    }

    public async Task<ApiResponse<Client>> GetAll()
    {
        return await _apiClient.Get<Client, ClientResponse>("Client/GetAll");
    }

    public async Task<ApiResponse<Client>> GetFinalConsumer()
    {
        return await _apiClient.Get<Client, ClientResponse>("Client/FinalConsumer");
    }

    public async Task<ApiResponse<Client>> Update(Client client)
    {
        List<EmailRequest> emails = [];
        List<PhoneRequest> phones = [];
        foreach (var email in client.EmailCollection.Emails) emails.Add(new(email.Id, email.Value, email.IsActive));
        foreach (var phone in client.PhoneCollection.Phones) phones.Add(new(phone.Id, phone.NationalId, phone.Value, phone.IsActive));
        UpdateClientRequest request = new(client.Id,
                                          client.Address.Country,
                                          client.Address.State,
                                          client.Address.City,
                                          client.Address.Street,
                                          client.Address.PostalCode,
                                          client.ClientType.Id,
                                          client.TaxCondition.Id,
                                          client.ClientName,
                                          client.Cuil,
                                          client.EmailCollection.HasEmailNotificationEnable,
                                          emails,
                                          client.PhoneCollection.HasPhoneNotificationEnable,
                                          phones,
                                          client.Observation,
                                          client.HasCheckingAccount,
                                          client.IsWholesaler);

        return await _apiClient.Post<Client, ClientResponse>("Client/Update", request);
    }
}
