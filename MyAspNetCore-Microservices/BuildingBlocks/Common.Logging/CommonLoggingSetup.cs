using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Common.Logging;

public static class CommonLoggingSetup
{
    public static Action<HostBuilderContext, LoggerConfiguration> AddCommonLoggingConfigure =>
        (context, configuration) =>
        {
            var elasticUrl = context.Configuration.GetValue<string>("ElasticConfiguration:Url");
            configuration.Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(
                    new ElasticsearchSinkOptions(new Uri(elasticUrl))
                    {
                        IndexFormat = $"applogs-{context.HostingEnvironment.ApplicationName}-{context.HostingEnvironment.EnvironmentName}-{DateTime.UtcNow:yyy-MM}",
                        AutoRegisterTemplate = true,
                        NumberOfShards = 2,
                        NumberOfReplicas = 1
                    })
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .ReadFrom.Configuration(context.Configuration);

        };
}