using Lubricentro25.Pages.Configuration.Views;
using Lubricentro25.ViewModels.Configurations;

namespace Lubricentro25.Pages.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddConfigurationPages(this IServiceCollection services)
    {

        //  *PAGES*  //
        //Employee
        services.AddSingleton<EmployeeConfigurationPage>();
        services.AddSingleton<EmployeeEditorView>();
        services.AddSingleton<RoleConfigurationPage>();

        //  *ViewModels*  //
        services.AddSingleton<EmployeeConfigurationViewModel>();
        services.AddSingleton<EmployeeEditorViewModel>();
        services.AddSingleton<RoleConfigurationsViewModel>();
        
        return services;
    }
}
