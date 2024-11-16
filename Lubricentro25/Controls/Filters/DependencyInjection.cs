using Lubricentro25.Controls.Filters.SellItems;
using System.Reflection;

namespace Lubricentro25.Controls.Filters;

public static class DependencyInjection
{
    public static IServiceCollection AddFilterViewModels(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(SellItemFilterViewModel));
        var viewmodelTypes = assembly!.GetTypes()
            .Where(t => t.Namespace != null && t.Namespace.StartsWith("Lubricentro25.Controls.Filters") && !t.IsAbstract && t.IsClass && t.Name != nameof(DependencyInjection));

        // Register each page dynamically as a singleton
        foreach (var viewmodelType in viewmodelTypes)
        {
            if(viewmodelType.Name.EndsWith("ViewModel"))
            services.AddSingleton(viewmodelType);
        }
        return services;
    }
}
