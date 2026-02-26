using System;
using MediatR;

namespace Orso.Arpa.Domain.MusicLibraryDomain.Notifications
{
    public class SetlistChangedNotification : INotification
    {
        public Guid SetlistId { get; set; }
        public string SetlistName { get; set; }
    }
}
