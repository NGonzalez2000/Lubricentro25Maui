namespace Lubricentro25.Api.Interface;

public interface ITaxConditionEndpoint
{
    Task<ApiResponse<TaxCondition>> GetTaxConditionsAsync();
    Task<ApiResponse<TaxCondition>> CreateTaxCondition(TaxCondition taxCondition);
    Task<ApiResponse<TaxCondition>> UpdateTaxCondition(TaxCondition taxCondition);
    Task<ApiResponse> DeleteTaxCondition(TaxCondition taxCondition);
}
