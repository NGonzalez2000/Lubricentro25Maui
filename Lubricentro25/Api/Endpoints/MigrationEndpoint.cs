using Lubricentro25.Api.Contracts.Customers;
using Lubricentro25.Api.Contracts.Email;
using Lubricentro25.Api.Contracts.Migrations;
using Lubricentro25.Api.Contracts.Phone;
using Lubricentro25.Api.Contracts.TaxCondition;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public class MigrationEndpoint(ILubricentroApiClient _apiClient) : IMigrationEndpoint
{
    public async Task<ApiResponse<TaxCondition>> GetTaxCondition()
    {
        return await _apiClient.Get<TaxCondition, TaxConditionResponse>("Migrations/TaxConditions");
    }
   
    public async Task<ApiResponse<TaxCondition>> CreateTaxCondition(TaxCondition taxCondition)
    {
        CreateTaxConditionMigrationRequest request = new(taxCondition.Id, taxCondition.Description, taxCondition.Type, taxCondition.Vat);
        return await _apiClient.Post<TaxCondition, TaxConditionResponse>("Migrations/TaxConditions/Create", request);
    }

    public async Task<ApiResponse<TaxCondition>> UpdateTaxCondition(TaxCondition taxCondition)
    {
        UpdateTaxConditionMigrationRequest request = new(taxCondition.Id, taxCondition.Description, taxCondition.Type, taxCondition.Vat);
        return await _apiClient.Post<TaxCondition, TaxConditionResponse>("Migrations/TaxConditions/Update", request);
    }

    public async Task<ApiResponse<Client>> GetClients()
    {
        return await _apiClient.Get<Client, OldClientResponse>("Migrations/Clients");
    }

    public async Task<ApiResponse<Client>> CreateClients(Client client)
    {
        List<EmailRequest> emails = [];
        List<PhoneRequest> phones = [];
        foreach (var email in client.EmailCollection.Emails) emails.Add(new(email.Id, email.Value, email.IsActive));
        foreach (var phone in client.PhoneCollection.Phones) phones.Add(new(phone.Id, phone.NationalId, phone.Value, phone.IsActive));
        CreateClientMigrationRequest request = new(client.Id,
                                                   client.Address.Country,
                                                   client.Address.State,
                                                   client.Address.City,
                                                   client.Address.Street,
                                                   client.Address.PostalCode,
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
        return await _apiClient.Post<Client, ClientResponse>("Migrations/Clients/Create", request);
    }

    public async Task<ApiResponse<Client>> UpdateClients(Client client)
    {
        List<EmailRequest> emails = [];
        List<PhoneRequest> phones = [];
        foreach (var email in client.EmailCollection.Emails) emails.Add(new(email.Id, email.Value, email.IsActive));
        foreach (var phone in client.PhoneCollection.Phones) phones.Add(new(phone.Id, phone.NationalId, phone.Value, phone.IsActive));
        UpdateClientMigrationRequest request = new(client.Id,
                                                   client.Address.Country,
                                                   client.Address.State,
                                                   client.Address.City,
                                                   client.Address.Street,
                                                   client.Address.PostalCode,
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
        return await _apiClient.Post<Client, ClientResponse>("Migrations/Clients/Update", request);
    }
}
