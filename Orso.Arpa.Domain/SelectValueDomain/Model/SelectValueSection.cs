using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.SectionDomain.Model;

namespace Orso.Arpa.Domain.SelectValueDomain.Model
{
    public class SelectValueSection : BaseEntity
    {
        public SelectValueSection(Guid id, Guid sectionId, Guid selectValueId) : base(id)
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

        [CascadingSoftDelete]
        public virtual ICollection<MusicianProfilePositionInner> MusicianProfilePositionsAsInner { get; private set; } = new HashSet<MusicianProfilePositionInner>();
        [CascadingSoftDelete]
        public virtual ICollection<MusicianProfilePositionTeam> MusicianProfilePositionsAsTeam { get; private set; } = new HashSet<MusicianProfilePositionTeam>();
    }
}
