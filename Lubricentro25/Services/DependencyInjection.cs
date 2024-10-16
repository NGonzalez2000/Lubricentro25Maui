using Lubricentro25.Services.Interfaces;

namespace Lubricentro25.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IPopUpService, PopUpService>();
        return services;
    }
}
