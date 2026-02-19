using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Api.Hubs;
using Orso.Arpa.Application.ChatApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Api.Workers;

public sealed class LiveLocationExpiryWorker : BackgroundService
{
    private readonly ILogger<LiveLocationExpiryWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHubContext<ChatHub> _hubContext;

    private static readonly TimeSpan CheckInterval = TimeSpan.FromSeconds(60);

    public LiveLocationExpiryWorker(
        ILogger<LiveLocationExpiryWorker> logger,
        IServiceProvider serviceProvider,
        IHubContext<ChatHub> hubContext)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CheckExpiredSharesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking expired live location shares");
            }

            await Task.Delay(CheckInterval, stoppingToken);
        }
    }

    private async Task CheckExpiredSharesAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IArpaContext>();

        var expiredShares = await context.ChatLiveLocationShares
            .Where(s => s.IsActive && !s.Deleted && s.ExpiresAt <= DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        if (expiredShares.Count == 0)
            return;

        _logger.LogInformation("Stopping {Count} expired live location shares", expiredShares.Count);

        foreach (var share in expiredShares)
        {
            share.Stop();

            await _hubContext.Clients.Group($"chat_{share.ChatRoomId}").SendAsync("LiveLocationStopped", new LiveLocationShareDto
            {
                Id = share.Id,
                ChatRoomId = share.ChatRoomId,
                UserId = share.UserId,
                MessageId = share.MessageId,
                Latitude = share.Latitude,
                Longitude = share.Longitude,
                Accuracy = share.Accuracy,
                StartedAt = share.StartedAt,
                ExpiresAt = share.ExpiresAt,
                LastUpdatedAt = share.LastUpdatedAt,
                IsActive = false
            }, cancellationToken);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
