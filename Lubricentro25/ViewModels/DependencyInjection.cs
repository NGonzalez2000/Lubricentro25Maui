using Lubricentro25.ViewModels.Chats;
using System.Reflection;

namespace Lubricentro25.ViewModels;

public static class DependencyInjection
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(ChatViewModel));
        var viewmodelTypes = assembly!.GetTypes()
            .Where(t => t.Namespace != null && t.Namespace.StartsWith("Lubricentro25.ViewModels") && !t.IsAbstract && t.IsClass && t.Name != nameof(DependencyInjection));

        // Register each page dynamically as a singleton
        foreach (var viewmodelType in viewmodelTypes)
        {
            services.AddSingleton(viewmodelType);
        }
        return services;
    }
}
