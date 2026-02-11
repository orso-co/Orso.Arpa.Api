using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orso.Arpa.Domain.UserDomain.Enums;

namespace Orso.Arpa.Domain.UserDomain.Interfaces
{
    public interface INotificationDispatcher
    {
        Task DispatchAsync(NotificationEventType eventType, IEnumerable<Guid> recipientUserIds,
            string title, string body, string url = null);
    }
}
