using Catalog.API.Configs;
using HealthChecks.UI.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var config = builder.Configuration;
builder.Services.AddHealthChecks()
    .AddMongoDb(config.GetValue<string>("DatabaseSettings:ConnectionString"), "Catalog Mongodb Health", HealthStatus.Degraded
        );
builder.Services.Configure<DatabaseSettings>(config.GetSection("DatabaseSettings"));

// setup services;
builder.Services.AddAppServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/hc", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
   Predicate = _ => true,
   ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

});

app.Run();