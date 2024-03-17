using Lubricentro25.Pages.Chats;
using Lubricentro25.Pages.Configuration;
using Lubricentro25.Pages.Configuration.Views;
using Lubricentro25.Pages.Login;

namespace Lubricentro25.Pages;

public static class DependencyInjection
{
    public static IServiceCollection AddPages(this IServiceCollection services)
    {
        services.AddSingleton<LoginPage>();
        services.AddSingleton<ChatPage>();
        services.AddSingleton<EmployeeConfigurationPage>();
        services.AddSingleton<RoleConfigurationPage>();
        services.AddViews();

        return services;
    }

    public static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddSingleton<EmployeeEditorView>();
        services.AddSingleton<RoleEditorView>();
        return services;
    }
}
