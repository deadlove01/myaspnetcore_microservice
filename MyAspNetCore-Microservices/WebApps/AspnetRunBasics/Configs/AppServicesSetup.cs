using AspnetRunBasics.Services;
using Common.Logging;

namespace AspnetRunBasics.Configs;

public static class AppServicesSetup
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = new ApiSettings();
        configuration.Bind("ApiSettings", apiSettings);

        services.AddTransient<LoggingHandler>();
        
        services.AddHttpClient<ICatalogService, CatalogService>(c =>
        {
            c.BaseAddress = new Uri(apiSettings.GatewayAddress);
        }).AddHttpMessageHandler<LoggingHandler>();
        
        services.AddHttpClient<IOrderService, OrderService>(c =>
        {
            c.BaseAddress = new Uri(apiSettings.GatewayAddress);
        }).AddHttpMessageHandler<LoggingHandler>();
        
        services.AddHttpClient<IBasketService, BasketService>(c =>
        {
            c.BaseAddress = new Uri(apiSettings.GatewayAddress);
        }).AddHttpMessageHandler<LoggingHandler>();

        return services;
    }
}