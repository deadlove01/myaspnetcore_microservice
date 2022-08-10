using AspnetRunBasics.Services;

namespace AspnetRunBasics.Configs;

public static class AppServicesSetup
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = new ApiSettings();
        configuration.Bind("ApiSettings", apiSettings);

        services.AddHttpClient<ICatalogService, CatalogService>(c =>
        {
            c.BaseAddress = new Uri(apiSettings.GatewayAddress);
        });
        
        services.AddHttpClient<IOrderService, OrderService>(c =>
        {
            c.BaseAddress = new Uri(apiSettings.GatewayAddress);
        });
        
        services.AddHttpClient<IBasketService, BasketService>(c =>
        {
            c.BaseAddress = new Uri(apiSettings.GatewayAddress);
        });

        return services;
    }
}