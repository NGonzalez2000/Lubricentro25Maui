using Lubricentro25.Api.Contracts.Brand;
using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public class BrandEndpoint(ILubricentroApiClient _client) : IBrandEndpoint
{
    public async Task<ApiResponse<Brand>> Create(Brand brand)
    {
        CreateBrandRequest request = new(brand);
        return await _client.Post<Brand, BrandResponse>("Brand/Create", request);
    }

    public async Task<ApiResponse> Delete(string Id)
    {
        DeleteBrandRequest request = new(Id);
        return await _client.Delete("Brand/Delete", request);
    }

    public async Task<ApiResponse<Brand>> GetALlAsync()
    {
        return await _client.Get<Brand, BrandResponse>("Brand/GetAll");
    }

    public async Task<ApiResponse<Brand>> Update(Brand brand)
    {
        UpdateBrandRequest request = new(brand);
        return await _client.Post<Brand, BrandResponse>("Brand/Update", request);
    }
}
