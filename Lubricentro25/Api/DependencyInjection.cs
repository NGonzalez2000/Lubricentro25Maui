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

        services.AddSingleton<ILubricentroApiClient, LubricentroApiClient>();

        services.AddSingleton<IEmployeeEndpoint, EmployeeEndpoint>();
        services.AddSingleton<IRoleEndpoint, RoleEndpoint>();
        services.AddSingleton<IChatEndpoint, ChatEndpoint>();
        services.AddSingleton<ICustomerEndpoint, CustomerEndpoint>();
        services.AddSingleton<IBrandEndpoint, BrandEndpoint>();
        services.AddSingleton<ITaxConditionEndpoint, TaxConditionEndpoint>();
        services.AddSingleton<IMigrationEndpoint, MigrationEndpoint>();
        services.AddSingleton<ICompanyEndpoint, CompanyEndpoint>();
        services.AddSingleton<IProviderEndpoint, ProviderEndpoint>();
        services.AddSingleton<IProductEndpoint, ProductEndpoint>();
        services.AddSingleton<IBranchEndpoint, BranchEndpoint>();
        services.AddSingleton<IVehicleFactoryEndpoint, VehicleFactoryEndpoint>();
        services.AddSingleton<IVehicleModelEndpoint, VehicleModelEndpoint>();

        return services;
    }
}
