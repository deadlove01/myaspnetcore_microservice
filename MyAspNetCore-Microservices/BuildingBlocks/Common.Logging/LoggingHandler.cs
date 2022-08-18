using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Common.Logging;

public class LoggingHandler : DelegatingHandler
{
    private readonly ILogger<LoggingHandler> _logger;

    public LoggingHandler(ILogger<LoggingHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var totalElapsedTime = Stopwatch.StartNew();
        
        if (request.Content != null)
        {
            var content = await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            Trace.WriteLine($"==>:\n {content}");
            _logger.LogInformation("Request data: {Content}", content);
        }
       
        var response = await base.SendAsync(request, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        Trace.WriteLine($"<==:\n {responseContent}");
        _logger.LogInformation("response data: {responseContent}", responseContent);

        totalElapsedTime.Stop();
        Debug.WriteLine($"Total elapsed time: {totalElapsedTime.ElapsedMilliseconds} ms");
        
        _logger.LogInformation("request & response Total elapsed time: {Total}", totalElapsedTime.ElapsedMilliseconds);
        return response;
    }
}