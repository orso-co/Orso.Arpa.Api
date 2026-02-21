using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Orso.Arpa.Api.Controllers
{
    public class SystemController : BaseController
    {
        private static readonly DateTime StartTime = DateTime.UtcNow;
        private readonly HealthCheckService _healthCheckService;

        public SystemController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet("info")]
        public async Task<IActionResult> GetInfo(CancellationToken cancellationToken)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                ?? assembly.GetName().Version?.ToString() ?? "unknown";
            var buildDate = System.IO.File.GetLastWriteTimeUtc(assembly.Location).ToString("yyyy-MM-dd HH:mm:ss UTC");

            var dbStatus = "Unknown";
            try
            {
                HealthReport report = await _healthCheckService.CheckHealthAsync(cancellationToken);
                dbStatus = report.Status == HealthStatus.Healthy ? "Healthy" : "Unhealthy";
            }
            catch
            {
                dbStatus = "Unhealthy";
            }

            var uptime = DateTime.UtcNow - StartTime;

            return Ok(new
            {
                Version = version,
                BuildDate = buildDate,
                Runtime = RuntimeInformation.FrameworkDescription,
                Os = $"{RuntimeInformation.OSDescription} ({RuntimeInformation.OSArchitecture})",
                Hostname = Environment.MachineName,
                StartTime = StartTime.ToString("yyyy-MM-dd HH:mm:ss UTC"),
                Uptime = FormatUptime(uptime),
                DbStatus = dbStatus,
            });
        }

        private static string FormatUptime(TimeSpan ts)
        {
            if (ts.TotalDays >= 1)
                return $"{(int)ts.TotalDays}d {ts.Hours}h {ts.Minutes}m";
            if (ts.TotalHours >= 1)
                return $"{(int)ts.TotalHours}h {ts.Minutes}m";
            return $"{(int)ts.TotalMinutes}m";
        }
    }
}
