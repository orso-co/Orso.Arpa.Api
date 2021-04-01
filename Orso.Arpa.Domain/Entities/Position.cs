using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Entities
{
    public class Position : BaseEntity
    {
        public string Name { get; private set; }
        public Guid SectionId { get; private set; }
        public virtual Section Section { get; private set; }
        public virtual ICollection<MusicianProfile> MusicianProfiles { get; private set; } = new HashSet<MusicianProfile>();
    }
}
