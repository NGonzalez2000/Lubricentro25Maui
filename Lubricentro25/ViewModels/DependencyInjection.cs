using Lubricentro25.Pages.Configuration.Views;
using Lubricentro25.ViewModels.Chats;
using Lubricentro25.ViewModels.Configurations;
using Lubricentro25.ViewModels.Login;

namespace Lubricentro25.ViewModels;

public static class DependencyInjection
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        // Page View Model
        services.AddSingleton<ChatViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<EmployeeConfigurationViewModel>();
        services.AddSingleton<RoleConfigurationsViewModel>();
        
        
        // View View Model
        services.AddSingleton<EmployeeEditorViewModel>();
        services.AddSingleton<RoleEditorViewModel>();
        return services;
    }
}
