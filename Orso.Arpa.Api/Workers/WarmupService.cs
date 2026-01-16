using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Orso.Arpa.Api.Workers;

/// <summary>
/// Background service that warms up the GraphQL engine at startup to avoid JIT compilation delays.
/// This is especially important on ARM64 (Raspberry Pi) where JIT is slower.
/// </summary>
public sealed class WarmupService : BackgroundService
{
    private readonly ILogger<WarmupService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private const string LoggerPrefix = "WARMUP:";

    public WarmupService(ILogger<WarmupService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Wait a bit for the app to fully start
        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

        _logger.LogInformation("{Prefix} Starting GraphQL warmup...", LoggerPrefix);

        try
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5000");
            client.Timeout = TimeSpan.FromSeconds(60);

            // Warmup queries - these trigger JIT compilation of GraphQL execution paths
            var warmupQueries = new[]
            {
                "{ __schema { types { name } } }",
                "{ projects { totalCount } }",
                "{ persons { totalCount } }",
                "{ appointments { totalCount } }",
                "{ sections { totalCount } }"
            };

            foreach (var query in warmupQueries)
            {
                if (stoppingToken.IsCancellationRequested) break;

                try
                {
                    var content = new StringContent(
                        $"{{\"query\":\"{query}\"}}",
                        Encoding.UTF8,
                        "application/json");

                    var response = await client.PostAsync("/graphql", content, stoppingToken);

                    _logger.LogDebug("{Prefix} Warmup query executed: {Query} -> {StatusCode}",
                        LoggerPrefix, query, response.StatusCode);
                }
                catch (Exception ex)
                {
                    _logger.LogDebug("{Prefix} Warmup query failed (expected if auth required): {Query} -> {Error}",
                        LoggerPrefix, query, ex.Message);
                }

                // Small delay between queries
                await Task.Delay(100, stoppingToken);
            }

            _logger.LogInformation("{Prefix} GraphQL warmup completed", LoggerPrefix);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "{Prefix} Warmup failed, but application will continue", LoggerPrefix);
        }
    }
}
