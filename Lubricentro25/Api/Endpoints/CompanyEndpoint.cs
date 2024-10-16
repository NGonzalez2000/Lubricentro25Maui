using Lubricentro25.Api.Contracts.Companies;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public class CompanyEndpoint(ILubricentroApiClient _client) : ICompanyEndpoint
{
    public async Task<ApiResponse<Company>> Create(Company company)
    {
        CreateCompanyRequest request = new(company.Name, company.Cuil, company.Email, company.ClientId, company.ClientSecret);
        return await _client.Post<Company, CompanyResponse>("company/create", request);
    }

    public async Task<ApiResponse> Delete(string Id)
    {
        DeleteCompanyRequest request = new(Id);
        return await _client.Delete("company/delete", request);
    }

    public async Task<ApiResponse<Company>> GetALlAsync()
    {
        return await _client.Get<Company, CompanyResponse>("company/getall");
    }

    public async Task<ApiResponse<Company>> Update(Company company)
    {
        UpdateCompanyRequest request = new(company.Id, company.Name, company.Cuil, company.Email, company.ClientId, company.ClientSecret);
        return await _client.Post<Company, CompanyResponse>("company/update", request);
    }
    public Task<ApiResponse<CompanyService>> GetServicesAsync()
    {
        return _client.Get<CompanyService, CompanyServiceResponse>("company/getservices");
    }


    public async Task<ApiResponse<List<CompanyService>>> UpdateCompanyServicesAsync(List<CompanyService> companyServiceRequests)
    {
        List<CompanyServiceRequest> services = [];
        foreach (CompanyService companyService in companyServiceRequests) 
        {
            services.Add(new(companyService.Id, companyService.Company.Id));
        }

        UpdateCompanyServicesRequest request = new(services);
        return await _client.Post<List<CompanyService>, List<CompanyServiceResponse>>("company/updateservices", request);

    }
}
