using Lubricentro25.Api.Contracts.Branch;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

internal class BranchEndpoint(ILubricentroApiClient apiClient) : IBranchEndpoint
{
    public async Task<ApiResponse<Branch>> CreateBranchAsync(Company company, Branch branch)
    {
        CreateBranchRequest request = new(company.Id, branch.Name, branch.PointOfSale, branch.Address.Country, branch.Address.Street, branch.Address.City, branch.Address.Street, branch.Address.PostalCode);
        return await apiClient.Post<Branch, BranchResponse>("Branch/Create", request);
    }

    public async Task<ApiResponse> DeleteBranchAsync(Branch branch)
    {
        DeleteBranchRequest request = new(branch.Id);
        return await apiClient.Delete("Branch/Delete", request);
    }

    public async Task<ApiResponse<Branch>> GetBranchesAsync()
    {
        return await apiClient.Get<Branch, BranchResponse>("Branch/GetAll");
    }

    public async Task<ApiResponse<List<Branch>>> GetComapnyBranchesAsync(Company company)
    {
        GetCompanyBranchesRequest request = new(company.Id);
        return await apiClient.Post<List<Branch>, List<BranchResponse>>("Branch/GetByCompanyId", request);
    }

    public async Task<ApiResponse<Branch>> UpdateBranchAsync(Branch branch)
    {
        UpdateBranchRequest request = new(branch.Id, branch.Name, branch.PointOfSale, branch.Address.Country, branch.Address.Street, branch.Address.City, branch.Address.Street, branch.Address.PostalCode);
        return await apiClient.Post<Branch, BranchResponse>("Branch/Update", request);
    }
}
