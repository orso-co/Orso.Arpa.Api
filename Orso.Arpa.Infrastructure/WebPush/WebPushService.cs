using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Interfaces;

namespace Orso.Arpa.Infrastructure.WebPush
{
    public class WebPushService : IWebPushService
    {
        private readonly IArpaContext _arpaContext;
        private readonly VapidConfiguration _vapidConfiguration;
        private readonly ILogger<WebPushService> _logger;

        public WebPushService(
            IArpaContext arpaContext,
            VapidConfiguration vapidConfiguration,
            ILogger<WebPushService> logger)
        {
            _arpaContext = arpaContext;
            _vapidConfiguration = vapidConfiguration;
            _logger = logger;
        }

        public async Task SendAsync(Guid userId, string title, string body, string url = null)
        {
            if (string.IsNullOrEmpty(_vapidConfiguration?.PublicKey) || string.IsNullOrEmpty(_vapidConfiguration?.PrivateKey))
            {
                _logger.LogWarning("VAPID keys not configured, skipping web push");
                return;
            }

            var subscriptions = await _arpaContext.PushSubscriptions
                .AsNoTracking()
                .Where(s => s.UserId == userId && !s.Deleted)
                .ToListAsync();

            if (!subscriptions.Any())
            {
                return;
            }

            // Format compatible with Angular Service Worker (NGSW)
            var payload = JsonSerializer.Serialize(new
            {
                notification = new
                {
                    title,
                    body,
                    icon = "/images/arpa/logos/logo-dark.png",
                    data = url != null ? new
                    {
                        onActionClick = new
                        {
                            @default = new { operation = "navigateLastFocusedOrOpen", url }
                        }
                    } : null
                }
            });

            var pushClient = new PushServiceClient();
            pushClient.DefaultAuthentication = new VapidAuthentication(
                _vapidConfiguration.PublicKey,
                _vapidConfiguration.PrivateKey)
            {
                Subject = _vapidConfiguration.Subject
            };

            foreach (var subscription in subscriptions)
            {
                try
                {
                    var pushSubscription = new Lib.Net.Http.WebPush.PushSubscription
                    {
                        Endpoint = subscription.Endpoint,
                        Keys = { ["p256dh"] = subscription.P256dh, ["auth"] = subscription.Auth }
                    };

                    var message = new PushMessage(payload)
                    {
                        Topic = $"arpa-{Guid.NewGuid():N}",
                        Urgency = PushMessageUrgency.Normal
                    };

                    await pushClient.RequestPushMessageDeliveryAsync(pushSubscription, message);
                }
                catch (PushServiceClientException ex) when (ex.StatusCode == HttpStatusCode.Gone)
                {
                    _logger.LogInformation("Push subscription expired for user {UserId}, endpoint {Endpoint}", userId, subscription.Endpoint);
                    subscription.Delete("system", DateTime.UtcNow);
                    await _arpaContext.SaveChangesAsync(CancellationToken.None);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send push notification to user {UserId}, endpoint {Endpoint}", userId, subscription.Endpoint);
                }
            }
        }
    }
}
