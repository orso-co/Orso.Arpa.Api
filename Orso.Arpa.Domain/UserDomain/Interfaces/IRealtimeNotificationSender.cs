using System;
using System.Threading.Tasks;

namespace Orso.Arpa.Domain.UserDomain.Interfaces
{
    public interface IRealtimeNotificationSender
    {
        Task SendToUserAsync(Guid userId, string title, string body, string url = null);
    }
}
