using Lubricentro25.Api.Interface;

namespace Lubricentro25.Api.Endpoints;

public static class DependencyInjection
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddSingleton<IEmployeeEndpoint, EmployeeEndpoint>();
        services.AddSingleton<IRoleEndpoint, RoleEndpoint>();
        services.AddSingleton<IAuthenticationEndpoint, AuthenticationEndpoint>();
        services.AddSingleton<IChatEndpoint, ChatEndpoint>();

        return services;
    }
}
