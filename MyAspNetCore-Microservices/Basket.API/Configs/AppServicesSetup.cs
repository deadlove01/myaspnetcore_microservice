using Basket.API.Repository;

namespace Basket.API.Configs;

public static class AppServicesSetup
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IBasketRepo, BasketRepo>();
        
        return services;
    }
}