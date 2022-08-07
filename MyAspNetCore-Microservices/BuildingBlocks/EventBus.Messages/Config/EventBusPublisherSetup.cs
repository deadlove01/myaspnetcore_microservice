using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Messages.Config;

public static class EventBusPublisherSetup
{
    public static IServiceCollection AddPublisherServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var eventBusSetting = new EventBusSetting();
        configuration.GetSection("EventBusSettings").Bind(eventBusSetting);

        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(eventBusSetting.HostAddress);
            });
        });

        services.AddMassTransitHostedService();
        return services;
    }
}