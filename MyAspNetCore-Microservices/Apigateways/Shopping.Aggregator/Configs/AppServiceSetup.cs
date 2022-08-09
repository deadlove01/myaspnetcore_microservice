using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.Configs;

public static class AppServiceSetup
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = new ApiSettings();
        configuration.Bind("ApiSettings", apiSettings);
        
        services.AddHttpClient<ICatalogService, CatalogService>(c =>
        {
            c.BaseAddress = new Uri(apiSettings.CatalogUrl);
        });
        
        services.AddHttpClient<IOrderService, OrderService>(c =>
        {
            c.BaseAddress = new Uri(apiSettings.OrderUrl);
        });
        
        services.AddHttpClient<IBasketService, BasketService>(c =>
        {
            c.BaseAddress = new Uri(apiSettings.BasketUrl);
        });

        return services;
    }
}