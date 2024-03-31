using Lubricentro25.Models.Helpers;
using Lubricentro25.Models.Helpers.Interface;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Lubricentro25.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddLubricentroApi(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        services.AddSingleton<IChatConnectionHelper, ChatConnectionHelper>();

        services.AddSingleton<ILubricentroApiClient, LubricentroApiClient>();

        return services;
    }
}
