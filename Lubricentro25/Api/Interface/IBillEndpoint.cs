namespace Lubricentro25.Api.Interface;

public interface IBillEndpoint
{
    Task<ApiResponse<BillType>> GetBillTypeAsync();
    Task<ApiResponse<BillType>> UpdateBillTypeAsync(BillType billType);
}
