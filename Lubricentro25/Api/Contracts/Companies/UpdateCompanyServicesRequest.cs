namespace Lubricentro25.Api.Contracts.Companies;

internal record UpdateCompanyServicesRequest(List<CompanyServiceRequest> CompanyServices);
internal record CompanyServiceRequest(string Id, string CompanyId);
