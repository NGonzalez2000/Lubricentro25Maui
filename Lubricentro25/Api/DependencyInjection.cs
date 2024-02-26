using Microsoft.Extensions.Options;

namespace Lubricentro25.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddLubricentroApi(this IServiceCollection services, Action<LubricentroClientOptions> options)
    {
        services.Configure(options);
        services.AddSingleton<ILubricentroApiClient, LubricentroApiClient>(
            provider => {
                var option = provider.GetRequiredService<IOptions<LubricentroClientOptions>>().Value;
                return new LubricentroApiClient(option);
                });
        return services;
    }
}
