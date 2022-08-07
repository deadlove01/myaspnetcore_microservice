// using MassTransit;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace EventBus.Messages.Config;
//
// public static class EventBusSubscriberSetup
// {
//     public static IServiceCollection AddSubscriberServices(this IServiceCollection services,
//         IConfiguration configuration)
//     {
//         var eventBusSetting = new EventBusSetting();
//         configuration.GetSection("EventBusSettings").Bind(eventBusSetting);
//
//         services.AddMassTransit(config =>
//         {
//             config.UsingRabbitMq((ctx, cfg) =>
//             {
//                 cfg.Host(eventBusSetting.HostAddress);
//             });
//         });
//
//         services.AddMassTransitHostedService();
//         
//         
//         // MassTransit-RabbitMQ Configuration
//         // services.AddMassTransit(config => {
//         //
//         //     config.AddConsumer<BasketCheckoutConsumer>();
//         //
//         //     config.UsingRabbitMq((ctx, cfg) => {
//         //         cfg.Host(Configuration["EventBusSettings:HostAddress"]);
//         //         cfg.UseHealthCheck(ctx);
//         //
//         //         cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c => {
//         //             c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
//         //         });
//         //     });
//         // });
//         // services.AddMassTransitHostedService();
//         return services;
//     }
// }