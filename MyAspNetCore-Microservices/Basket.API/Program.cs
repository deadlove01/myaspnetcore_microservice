using Basket.API.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;
var redisSetting = new RedisSetting();
var redisSettingConfig = config.GetSection("RedisSetting");
redisSettingConfig.Bind(redisSetting);

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = redisSetting.Url;
});

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

app.Run();
