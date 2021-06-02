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
        public virtual ICollection<MusicianProfilePositionInner> MusicianProfilePositionsAsInner { get; private set; } = new HashSet<MusicianProfilePositionInner>();
        public virtual ICollection<MusicianProfilePositionTeam> MusicianProfilePositionsAsTeam { get; private set; } = new HashSet<MusicianProfilePositionTeam>();
    }
}
