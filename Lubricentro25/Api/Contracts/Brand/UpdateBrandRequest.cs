namespace Lubricentro25.Api.Contracts.Brand;

public record UpdateBrandRequest(string Id, string Name, List<string> ProvidersId)
{
    public UpdateBrandRequest(Models.Brand brand) : this(brand.Id, brand.Name, [])
    {
        foreach (var provider in brand.Providers)
        {
            ProvidersId.Add(provider.Id);
        }
    }
}
