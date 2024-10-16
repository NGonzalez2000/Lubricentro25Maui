using Lubricentro25.Api.Contracts.TaxCondition;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public class TaxConditionEndpoint(ILubricentroApiClient _apiClient) : ITaxConditionEndpoint

{
    public async Task<ApiResponse<TaxCondition>> CreateTaxCondition(TaxCondition taxCondition)
    {
        CreateTaxConditionRequest request = new(taxCondition.Description, taxCondition.Type, taxCondition.Vat);
        return await _apiClient.Post<TaxCondition, TaxConditionResponse>("TaxCondition/Create", request);
    }
    public async Task<ApiResponse<TaxCondition>> UpdateTaxCondition(TaxCondition taxCondition)
    {
        UpdateTaxConditionRequest request = new(taxCondition.Id, taxCondition.Description, taxCondition.Type, taxCondition.Vat);
        return await _apiClient.Post<TaxCondition, TaxConditionResponse>("TaxCondition/Update", request);
    }

    public async Task<ApiResponse> DeleteTaxCondition(TaxCondition taxCondition)
    {
        DeleteTaxConditionRequest request = new(taxCondition.Id);
        return await _apiClient.Delete("TaxCondition/Delete", request);
    }

    public async Task<ApiResponse<TaxCondition>> GetTaxConditionsAsync()
    {
        return await _apiClient.Get<TaxCondition, TaxConditionResponse>("TaxCondition/GetAll");
    }

}
