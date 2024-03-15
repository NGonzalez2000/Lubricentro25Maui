using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Lubricentro25.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddLubricentroApi(this IServiceCollection services, Action<LubricentroClientOptions> options)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        services.Configure(options);
        services.AddSingleton<ILubricentroApiClient, LubricentroApiClient>(
            provider => {
                var option = provider.GetRequiredService<IOptions<LubricentroClientOptions>>().Value;
                var mapper = provider.GetRequiredService<IMapper>();
                return new LubricentroApiClient(mapper, option);
                });

        return services;
    }
}
