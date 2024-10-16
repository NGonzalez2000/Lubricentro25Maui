namespace Lubricentro25.Api.Interface;

public interface IMigrationEndpoint
{
    Task<ApiResponse<TaxCondition>> GetTaxCondition();
    Task<ApiResponse<TaxCondition>> CreateTaxCondition(TaxCondition taxCondition);
    Task<ApiResponse<TaxCondition>> UpdateTaxCondition(TaxCondition taxCondition);

    Task<ApiResponse<Client>> GetClients();
    Task<ApiResponse<Client>> CreateClients(Client client);
    Task<ApiResponse<Client>> UpdateClients(Client client);
}
