using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orso.Arpa.Application.PushNotificationApplication.Model;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Api.Controllers
{
    public class NotificationPreferencesController : BaseController
    {
        private readonly IArpaContext _arpaContext;
        private readonly ITokenAccessor _tokenAccessor;

        public NotificationPreferencesController(IArpaContext arpaContext, ITokenAccessor tokenAccessor)
        {
            _arpaContext = arpaContext;
            _tokenAccessor = tokenAccessor;
        }

        /// <summary>
        /// Gets the notification preferences for the current user
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<NotificationPreferenceDto>>> Get()
        {
            var userId = _tokenAccessor.UserId;

            var preferences = await _arpaContext.NotificationPreferences
                .AsNoTracking()
                .Where(p => p.UserId == userId && !p.Deleted)
                .Select(p => new NotificationPreferenceDto
                {
                    EventType = p.EventType,
                    Channels = p.Channels
                })
                .ToListAsync();

            return Ok(preferences);
        }

        /// <summary>
        /// Sets or updates notification preferences for the current user
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Put([FromBody] IEnumerable<NotificationPreferenceDto> dtos)
        {
            var userId = _tokenAccessor.UserId;

            var existingPreferences = await _arpaContext.NotificationPreferences
                .Where(p => p.UserId == userId && !p.Deleted)
                .ToListAsync();

            foreach (var dto in dtos)
            {
                var existing = existingPreferences.FirstOrDefault(p => p.EventType == dto.EventType);
                if (existing != null)
                {
                    existing.Channels = dto.Channels;
                }
                else
                {
                    var preference = new NotificationPreference(null, userId, dto.EventType, dto.Channels);
                    _ = _arpaContext.Add(preference);
                }
            }

            await _arpaContext.SaveChangesAsync(CancellationToken.None);

            return NoContent();
        }
    }
}
