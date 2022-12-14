using Basket.API.GrpcServices;
using Basket.API.Repository;
using Discount.Grpc.Protos;
using EventBus.Messages.Config;
using MassTransit;

namespace Basket.API.Configs;

public static class AppServicesSetup
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Program));
        
        services.AddScoped<IBasketRepo, BasketRepo>();
       
        
        var grpcSetting = new GrpcSetting();
        var grpcSettingConfig = configuration.GetSection("GrpcSetting");
        grpcSettingConfig.Bind(grpcSetting);

        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
        {
            opt.Address = new Uri(grpcSetting.Url);
        });

        services.AddPublisherServices(configuration);

        services.AddScoped<IDiscountService, DiscountService>();
        
        return services;
    }
}