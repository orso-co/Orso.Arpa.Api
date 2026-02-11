using System;

namespace Orso.Arpa.Domain.UserDomain.Enums
{
    [Flags]
    public enum NotificationChannel
    {
        None = 0,
        Push = 1,
        Email = 2,
        InApp = 4
    }
}
