using Lubricentro25.Pages.Chats;
using Lubricentro25.Pages.Configuration;
using Lubricentro25.Pages.Configuration.Migrations;
using Lubricentro25.Pages.Configuration.Views;
using Lubricentro25.Pages.Customers;
using Lubricentro25.Pages.Items;
using Lubricentro25.Pages.Login;
using Lubricentro25.Pages.Providers;
using System.Reflection;

namespace Lubricentro25.Pages;

public static class DependencyInjection
{
    public static IServiceCollection AddPages(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(LoginPage));
        var pageTypes = assembly!.GetTypes()
            .Where(t => t.Namespace != null && t.Namespace.StartsWith("Lubricentro25.Pages") && !t.IsAbstract && t.IsClass && t.Name != nameof(DependencyInjection));

        // Register each page dynamically as a singleton
        foreach (var pageType in pageTypes)
        {
            services.AddSingleton(pageType);
        }


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
