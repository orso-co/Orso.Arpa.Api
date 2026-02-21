using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Orso.Arpa.Domain.ProjectDomain.Enums;
using Orso.Arpa.Persistence.DataAccess;

namespace Orso.Arpa.Api.Controllers
{
    public class SystemController : BaseController
    {
        private static readonly DateTime StartTime = DateTime.UtcNow;
        private readonly HealthCheckService _healthCheckService;
        private readonly ArpaContext _arpaContext;
        private readonly IHostEnvironment _hostEnvironment;

        public SystemController(HealthCheckService healthCheckService, ArpaContext arpaContext, IHostEnvironment hostEnvironment)
        {
            _healthCheckService = healthCheckService;
            _arpaContext = arpaContext;
            _hostEnvironment = hostEnvironment;
        }

        [AllowAnonymous]
        [HttpGet("info")]
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> GetInfo(CancellationToken cancellationToken)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var informationalVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
                ?? assembly.GetName().Version?.ToString() ?? "unknown";
            var version = informationalVersion.Split('+')[0];
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
            var process = Process.GetCurrentProcess();

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
                Environment = _hostEnvironment.EnvironmentName,
                CpuCores = Environment.ProcessorCount,
                MemoryUsedMb = process.WorkingSet64 / 1024 / 1024,
                TotalMemoryMb = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes / 1024 / 1024,
                ThreadCount = process.Threads.Count,
                GcCollections = new { Gen0 = GC.CollectionCount(0), Gen1 = GC.CollectionCount(1), Gen2 = GC.CollectionCount(2) },
            });
        }

        [HttpGet("stats")]
        [ResponseCache(NoStore = true)]
        public async Task<IActionResult> GetStats(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var day1 = now.AddDays(-1);
            var day7 = now.AddDays(-7);
            var day30 = now.AddDays(-30);

            var auditLogs24h = await _arpaContext.AuditLogs.CountAsync(a => a.CreatedAt >= day1, cancellationToken);
            var auditLogs7d = await _arpaContext.AuditLogs.CountAsync(a => a.CreatedAt >= day7, cancellationToken);
            var auditLogs30d = await _arpaContext.AuditLogs.CountAsync(a => a.CreatedAt >= day30, cancellationToken);

            var usersTotal = await _arpaContext.Users.CountAsync(cancellationToken);
            var activeUsers30d = await _arpaContext.RefreshTokens
                .Where(r => r.CreatedOn >= day30)
                .Select(r => r.UserId)
                .Distinct()
                .CountAsync(cancellationToken);

            var projectsCompleted = await _arpaContext.Projects.CountAsync(p => !p.Deleted && p.IsCompleted, cancellationToken);
            var projectsOpen = await _arpaContext.Projects.CountAsync(p => !p.Deleted && !p.IsCompleted && p.Status != ProjectStatus.Cancelled, cancellationToken);

            var appointmentsTotal = await _arpaContext.Appointments.CountAsync(cancellationToken);
            var projectParticipationsTotal = await _arpaContext.ProjectParticipations.CountAsync(cancellationToken);
            var appointmentParticipationsTotal = await _arpaContext.AppointmentParticipations.CountAsync(cancellationToken);
            var personsTotal = await _arpaContext.Persons.CountAsync(cancellationToken);
            var chatMessagesTotal = await _arpaContext.ChatMessages.CountAsync(cancellationToken);

            return Ok(new
            {
                AuditLogs24h = auditLogs24h,
                AuditLogs7d = auditLogs7d,
                AuditLogs30d = auditLogs30d,
                UsersTotal = usersTotal,
                ActiveUsers30d = activeUsers30d,
                ProjectsCompleted = projectsCompleted,
                ProjectsOpen = projectsOpen,
                AppointmentsTotal = appointmentsTotal,
                ProjectParticipationsTotal = projectParticipationsTotal,
                AppointmentParticipationsTotal = appointmentParticipationsTotal,
                PersonsTotal = personsTotal,
                ChatMessagesTotal = chatMessagesTotal,
            });
        }

        [AllowAnonymous]
        [HttpGet("client-ip")]
        public IActionResult GetClientIp()
        {
            var ip = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "unknown";
            return Ok(new { Ip = ip });
        }

        private static string FormatUptime(TimeSpan ts)
        {
            if (ts.TotalDays >= 1)
                return $"{(int)ts.TotalDays}d {ts.Hours}h {ts.Minutes}m";
            if (ts.TotalHours >= 1)
                return $"{(int)ts.TotalHours}h {ts.Minutes}m";
            if (ts.TotalMinutes >= 1)
                return $"{(int)ts.TotalMinutes}m";
            return $"{(int)ts.TotalSeconds}s";
        }
    }
}
