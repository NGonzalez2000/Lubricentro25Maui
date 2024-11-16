using Lubricentro25.Api.Contracts.BillTypes;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public class BillEndpoint(ILubricentroApiClient apiClient) : IBillEndpoint
{
    public async Task<ApiResponse<BillType>> GetBillTypeAsync()
    {
        return await apiClient.Get<BillType, BillTypeResponse>("Bill/Types");
    }

    public async Task<ApiResponse<BillType>> UpdateBillTypeAsync(BillType billType)
    {
        UpdateBillTypeRequest request = new(billType.Id, billType.Description);
        return await apiClient.Post<BillType, BillTypeResponse>("Bill/Types/Update", request);
    }
}
