
using Discount.API.Repos;

namespace Discount.API.Configs;

public static class AppServicesSetup
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IDiscountRepo, DiscountRepo>();
        
        return services;
    }
}