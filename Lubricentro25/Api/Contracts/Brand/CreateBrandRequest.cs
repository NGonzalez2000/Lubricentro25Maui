namespace Lubricentro25.Api.Contracts.Brand;

public record CreateBrandRequest(string Name, List<string> ProvidersId)
{
    public CreateBrandRequest(Models.Brand brand) : this(brand.Name, [])
    {
        foreach(Provider provider in brand.Providers)
        {
            ProvidersId.Add(provider.Id);
        }
    }
}
