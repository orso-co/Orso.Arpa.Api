using MediatR;
using Orso.Arpa.Domain.MediathekDomain.Model;

namespace Orso.Arpa.Domain.MediathekDomain.Notifications
{
    public class MediathekAccessGrantedNotification : INotification
    {
        public MediathekAccess MediathekAccess { get; set; }
    }
}
