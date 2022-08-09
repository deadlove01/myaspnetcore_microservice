using Ocelot.Middleware;
using Ocelot.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((ctx, config) =>
{
    config.AddJsonFile($"ocelot.{ctx.HostingEnvironment.EnvironmentName}.json", false, true);
});

builder.Services.AddControllers();

builder.Services.AddOcelot();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseOcelot().Wait();

app.UseAuthorization();
app.MapControllers();

app.Run();