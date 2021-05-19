using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Entities
{
    public class InstrumentPart : BaseEntity
    {
        public InstrumentPart(Guid? id, Guid sectionId, Guid selectValueId) : base(id)
        {
            SectionId = sectionId;
            SelectValueId = selectValueId;
        }

        protected InstrumentPart()
        {
        }

        public Guid SectionId { get; private set; }
        public virtual Section Section { get; private set; }
        public Guid SelectValueId { get; private set; }
        public virtual SelectValue SelectValue { get; private set; }
        public virtual ICollection<MusicianProfile> MusicianProfiles { get; private set; } = new HashSet<MusicianProfile>();
    }
}
