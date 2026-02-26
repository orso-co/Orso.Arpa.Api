using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.PushNotificationApplication.Model;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Infrastructure.Authorization;

namespace Orso.Arpa.Api.Controllers
{
    public class PushSubscriptionsController : BaseController
    {
        private readonly IArpaContext _arpaContext;
        private readonly ITokenAccessor _tokenAccessor;
        private readonly VapidConfiguration _vapidConfiguration;
        private readonly IWebPushService _webPushService;

        public PushSubscriptionsController(
            IArpaContext arpaContext,
            ITokenAccessor tokenAccessor,
            VapidConfiguration vapidConfiguration,
            IWebPushService webPushService)
        {
            _arpaContext = arpaContext;
            _tokenAccessor = tokenAccessor;
            _vapidConfiguration = vapidConfiguration;
            _webPushService = webPushService;
        }

        /// <summary>
        /// Gets the VAPID public key for push subscription
        /// </summary>
        [AllowAnonymous]
        [HttpGet("vapid-public-key")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<object> GetVapidPublicKey()
        {
            return Ok(new { publicKey = _vapidConfiguration?.PublicKey });
        }

        /// <summary>
        /// Registers a push subscription for the current user
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] PushSubscriptionCreateDto dto)
        {
            var userId = _tokenAccessor.UserId;

            // Check if subscription with this endpoint already exists
            var existing = await _arpaContext.PushSubscriptions
                .FirstOrDefaultAsync(s => s.Endpoint == dto.Endpoint && !s.Deleted);

            if (existing != null)
            {
                if (existing.UserId == userId)
                {
                    return NoContent();
                }
                // Endpoint belongs to different user - remove old and create new
                existing.Delete("system", DateTime.UtcNow);
            }

            var subscription = new PushSubscription(null, userId, dto.Endpoint, dto.P256dh, dto.Auth, dto.UserAgent);
            _ = _arpaContext.Add(subscription);
            await _arpaContext.SaveChangesAsync(CancellationToken.None);

            return NoContent();
        }

        /// <summary>
        /// Removes a push subscription
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete([FromQuery] string endpoint)
        {
            var userId = _tokenAccessor.UserId;

            var subscription = await _arpaContext.PushSubscriptions
                .FirstOrDefaultAsync(s => s.Endpoint == endpoint && s.UserId == userId && !s.Deleted);

            if (subscription != null)
            {
                subscription.Delete(_tokenAccessor.UserName, DateTime.UtcNow);
                await _arpaContext.SaveChangesAsync(CancellationToken.None);
            }

            return NoContent();
        }

        /// <summary>
        /// Sends a test push notification to the current user
        /// </summary>
        [HttpPost("test")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> SendTestPush()
        {
            await _webPushService.SendAsync(
                _tokenAccessor.UserId,
                "ARPA Test",
                "Push-Benachrichtigungen funktionieren!",
                "/#/");

            return NoContent();
        }
    }
}
