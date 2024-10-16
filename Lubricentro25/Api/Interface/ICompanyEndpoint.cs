using Lubricentro25.Api.Contracts.Companies;

namespace Lubricentro25.Api.Interface;

public interface ICompanyEndpoint
{
    Task<ApiResponse<CompanyService>> GetServicesAsync();
    Task<ApiResponse<List<CompanyService>>> UpdateCompanyServicesAsync(List<CompanyService> companyServiceRequests);
    Task<ApiResponse<Company>> GetALlAsync();
    Task<ApiResponse<Company>> Create(Company company);
    Task<ApiResponse<Company>> Update(Company company);
    Task<ApiResponse> Delete(string Id);
}
