using Lubricentro25.Api.Contracts.Providers;

namespace Lubricentro25.Api.Contracts.Brand;

public record BrandResponse(string Id, string Name, List<ProviderResponse> Providers)
{
}
