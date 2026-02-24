using System;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.AnnouncementDomain.Model
{
    [HardDelete]
    public class AnnouncementRead
    {
        public Guid Id { get; set; }
        public DateTime ReadAt { get; set; }
        public bool TickerPinned { get; set; }

        public Guid AnnouncementId { get; set; }
        public virtual Announcement Announcement { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
