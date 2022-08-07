using EventBus.Messages.Common;
using EventBus.Messages.Config;
using MassTransit;
using Order.API.EventBusConsumer;

namespace Order.API.Configs;

public static class AppServicesSetup
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Program));
        
        // MassTransit-RabbitMQ Configuration
        var eventBusSetting = new EventBusSetting();
        configuration.GetSection("EventBusSettings").Bind(eventBusSetting);
        services.AddMassTransit(config => {

            config.AddConsumer<BasketCheckoutConsumer>();

            config.UsingRabbitMq((ctx, cfg) => {
                cfg.Host(eventBusSetting.HostAddress);

                cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
                    c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                });
            });
        });
        services.AddMassTransitHostedService();
        
        return services;
    }
}