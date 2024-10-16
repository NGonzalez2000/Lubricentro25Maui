namespace Lubricentro25.Api.Interface;

public interface IBranchEndpoint
{
    Task<ApiResponse<Branch>> GetBranchesAsync();
    Task<ApiResponse<List<Branch>>> GetComapnyBranchesAsync(Company company);
    Task<ApiResponse<Branch>> CreateBranchAsync(Company company, Branch branch);
    Task<ApiResponse<Branch>> UpdateBranchAsync(Branch branch);
    Task<ApiResponse> DeleteBranchAsync(Branch branch);

}
