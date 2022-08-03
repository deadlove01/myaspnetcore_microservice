using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Contracts.Persistence;

namespace Order.Application;

public static class OrderApplicationSetup
{
    public static IServiceCollection AddOrderApplicationService(this IServiceCollection services, IConfiguration configuration)
    {
        
        return services;
    }
}