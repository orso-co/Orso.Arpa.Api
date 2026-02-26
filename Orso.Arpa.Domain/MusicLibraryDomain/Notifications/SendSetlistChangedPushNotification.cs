using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Domain.UserDomain.Interfaces;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Notifications
{
    public class SendSetlistChangedPushNotification : INotificationHandler<SetlistChangedNotification>
    {
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IArpaContext _arpaContext;
        private readonly ILogger<SendSetlistChangedPushNotification> _logger;

        public SendSetlistChangedPushNotification(
            INotificationDispatcher notificationDispatcher,
            IArpaContext arpaContext,
            ILogger<SendSetlistChangedPushNotification> logger)
        {
            _notificationDispatcher = notificationDispatcher;
            _arpaContext = arpaContext;
            _logger = logger;
        }

        public async Task Handle(SetlistChangedNotification notification, CancellationToken cancellationToken)
        {
            // Get section IDs of all parts in the setlist's music pieces
            List<Guid> sectionIds = await _arpaContext.SetlistPieces
                .AsNoTracking()
                .Where(sp => sp.SetlistId == notification.SetlistId && !sp.Deleted)
                .SelectMany(sp => sp.MusicPiece.Parts)
                .Where(p => !p.Deleted && p.SectionId != null)
                .Select(p => p.SectionId.Value)
                .Distinct()
                .ToListAsync(cancellationToken);

            if (!sectionIds.Any())
            {
                return;
            }

            // Find performers whose instrument matches any section in the setlist
            List<Guid> recipientUserIds = await _arpaContext.MusicianProfiles
                .AsNoTracking()
                .Where(mp => !mp.Deleted && sectionIds.Contains(mp.InstrumentId))
                .Select(mp => mp.Person)
                .Where(p => p != null && !p.Deleted && p.User != null)
                .Select(p => p.User.Id)
                .Distinct()
                .ToListAsync(cancellationToken);

            if (!recipientUserIds.Any())
            {
                return;
            }

            await _notificationDispatcher.DispatchAsync(
                NotificationEventType.SetlistChanged,
                recipientUserIds,
                "Setlist aktualisiert",
                notification.SetlistName,
                "/#/staff/music-library");
        }
    }
}
