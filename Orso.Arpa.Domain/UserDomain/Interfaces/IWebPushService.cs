using System;
using System.Threading.Tasks;

namespace Orso.Arpa.Domain.UserDomain.Interfaces
{
    public interface IWebPushService
    {
        Task SendAsync(Guid userId, string title, string body, string url = null);
    }
}
