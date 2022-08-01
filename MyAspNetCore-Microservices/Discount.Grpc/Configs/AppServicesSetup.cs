using Discount.Shared.Repos;

namespace Discount.Grpc.Configs;

public static class AppServicesSetup
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IDiscountRepo, DiscountRepo>();
        
        return services;
    }
}