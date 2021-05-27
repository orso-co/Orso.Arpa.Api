using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Entities
{
    public class SelectValueSection : BaseEntity
    {
        public SelectValueSection(Guid? id, Guid sectionId, Guid selectValueId) : base(id)
        {
            SectionId = sectionId;
            SelectValueId = selectValueId;
        }

        protected SelectValueSection()
        {
        }

        public Guid SectionId { get; private set; }
        public virtual Section Section { get; private set; }
        public Guid SelectValueId { get; private set; }
        public virtual SelectValue SelectValue { get; private set; }
        public virtual ICollection<MusicianProfilePositionPerformer> MusicianProfilePositionsAsPerformer { get; private set; } = new HashSet<MusicianProfilePositionPerformer>();
        public virtual ICollection<MusicianProfilePositionStaff> MusicianProfilePositionsAsStaff { get; private set; } = new HashSet<MusicianProfilePositionStaff>();
    }
}
