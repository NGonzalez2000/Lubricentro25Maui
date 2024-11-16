using Lubricentro25.Api.Endpoints;
using Lubricentro25.Api.Interface;
using Lubricentro25.Models.Helpers;
using Lubricentro25.Models.Helpers.Interface;
using Mapster;
using MapsterMapper;
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

        //services.AddSingleton<ILubricentroApiClient, LubricentroApiClient>();

        //services.AddSingleton<IEmployeeEndpoint, EmployeeEndpoint>();
        //services.AddSingleton<IRoleEndpoint, RoleEndpoint>();

        //services.AddSingleton<IChatEndpoint, ChatEndpoint>();
        //services.AddSingleton<ICustomerEndpoint, CustomerEndpoint>();
        //services.AddSingleton<IClientTypeEndpoint, ClientTypeEndpoint>();
        //services.AddSingleton<IBranchEndpoint, BranchEndpoint>();
        //services.AddSingleton<IBrandEndpoint, BrandEndpoint>();
        //services.AddSingleton<ITaxConditionEndpoint, TaxConditionEndpoint>();
        //services.AddSingleton<IMigrationEndpoint, MigrationEndpoint>();
        //services.AddSingleton<ICompanyEndpoint, CompanyEndpoint>();
        //services.AddSingleton<IProviderEndpoint, ProviderEndpoint>();
        //services.AddSingleton<IProductEndpoint, ProductEndpoint>();
        //services.AddSingleton<IStockEndpoint, StockEndpoint>();
        //services.AddSingleton<IVehicleFactoryEndpoint, VehicleFactoryEndpoint>();
        //services.AddSingleton<IVehicleModelEndpoint, VehicleModelEndpoint>();
        //services.AddSingleton<IVatTypeEndpoint, VatTypeEndpoint>();
        //services.AddSingleton<IPaymentEndpoint, PaymentEndpoint>();
        //services.AddSingleton<IBillEndpoint, BillEndpoint>();

        var assembly = Assembly.GetExecutingAssembly();

        // Find all types in the specified namespace
        var typesInNamespace = assembly.GetTypes()
            .Where(t => t.Namespace != null && t.Namespace.StartsWith("Lubricentro25.Api") && t.IsInterface)
            .ToList();

        foreach (var interfaceType in typesInNamespace)
        {
            // Get the corresponding implementation
            var implementationType = assembly.GetTypes()
                .FirstOrDefault(t => interfaceType.IsAssignableFrom(t) && t.IsClass);

            if (implementationType != null)
            {
                // Register the interface with the implementation as a Singleton
                services.AddSingleton(interfaceType, implementationType);
            }
        }

        return services;
    }
}
