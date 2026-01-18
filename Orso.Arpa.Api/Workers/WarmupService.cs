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

    /// <summary>
    /// Delay before starting warmup to allow the app to fully initialize
    /// (database migrations, middleware pipeline, etc.)
    /// </summary>
    private static readonly TimeSpan StartupDelay = TimeSpan.FromSeconds(15);

    public WarmupService(ILogger<WarmupService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    private static string GetLocalApiBaseUrl()
    {
        // Read from ASPNETCORE_URLS environment variable, fallback to default
        var urls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
        if (!string.IsNullOrEmpty(urls))
        {
            // Take the first URL if multiple are configured
            var firstUrl = urls.Split(';')[0];
            // Replace 0.0.0.0 with localhost for internal access
            return firstUrl.Replace("0.0.0.0", "localhost");
        }
        return "http://localhost:5000";
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Wait for the app to fully start (including database migrations)
        _logger.LogInformation("{Prefix} Waiting {Delay} for app to fully start...", LoggerPrefix, StartupDelay);
        await Task.Delay(StartupDelay, stoppingToken);

        _logger.LogInformation("{Prefix} Starting GraphQL warmup...", LoggerPrefix);

        try
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(GetLocalApiBaseUrl());
            client.Timeout = TimeSpan.FromSeconds(120);

            // Warmup queries - these trigger JIT compilation of GraphQL execution paths
            // Note: Some queries will return 401 (auth required) but still warm up the code
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
                    using var content = new StringContent(
                        $"{{\"query\":\"{query}\"}}",
                        Encoding.UTF8,
                        "application/json");

                    using var response = await client.PostAsync("/graphql", content, stoppingToken);

                    _logger.LogInformation("{Prefix} Warmup query: {Query} -> {StatusCode}",
                        LoggerPrefix, query[..Math.Min(40, query.Length)], response.StatusCode);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation(ex, "{Prefix} Warmup query failed: {Query}",
                        LoggerPrefix, query[..Math.Min(40, query.Length)]);
                }

                // Delay between queries to allow JIT to complete
                await Task.Delay(500, stoppingToken);
            }

            _logger.LogInformation("{Prefix} GraphQL warmup completed successfully", LoggerPrefix);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "{Prefix} Warmup failed, but application will continue", LoggerPrefix);
        }
    }
}
