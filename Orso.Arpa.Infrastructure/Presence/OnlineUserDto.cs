using System;

namespace Orso.Arpa.Infrastructure.Presence
{
    public class OnlineUserDto
    {
        public Guid UserId { get; set; }
        public Guid PersonId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? InstrumentName { get; set; }
        public DateTime ConnectedAt { get; set; }
    }
}
