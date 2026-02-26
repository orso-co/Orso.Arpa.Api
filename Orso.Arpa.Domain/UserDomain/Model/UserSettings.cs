using System;

namespace Orso.Arpa.Domain.UserDomain.Model
{
    public class UserSettings
    {
        public Guid UserId { get; set; }
        public bool IsDarkMode { get; set; } = true;
        public string Language { get; set; } = "de";
        public bool SoundOnUserOnline { get; set; } = false;
        public bool SoundOnAnnouncement { get; set; } = false;

        public virtual User User { get; set; }
    }
}
