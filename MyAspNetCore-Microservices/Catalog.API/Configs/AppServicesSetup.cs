using Catalog.API.Data;
using Catalog.API.Repository;

namespace Catalog.API.Configs;

public static class AppServicesSetup
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {

        services.AddScoped<ICatalogContext, CatalogContext>();
        services.AddScoped<IProductRepo, ProductRepo>();
        
        return services;
    }
}