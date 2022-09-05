using Basket.API.Configs;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;
var redisSetting = new RedisSetting();
var redisSettingConfig = config.GetSection("CacheSettings");
redisSettingConfig.Bind(redisSetting);

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = redisSetting.ConnectionString;
});

builder.Services.AddHealthChecks()
    .AddRedis(config["CacheSettings:ConnectionString"], "Redis Health", HealthStatus.Degraded);        

builder.Services.AddAppServices(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/hc", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

});

app.Run();
