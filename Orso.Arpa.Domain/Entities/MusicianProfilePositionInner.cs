using System;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfilePositionInner : BaseEntity
    {
        public MusicianProfilePositionInner(Guid selectValueSectionId, Guid musicianProfileId, Guid? id = null) : base(id)
        {
            SelectValueSectionId = selectValueSectionId;
            MusicianProfileId = musicianProfileId;
        }

        protected MusicianProfilePositionInner()
        {

        }

        public Guid SelectValueSectionId { get; set; }
        public virtual SelectValueSection SelectValueSection { get; set; }
        public Guid MusicianProfileId { get; set; }
        public virtual MusicianProfile MusicianProfile { get; set; }
    }
}
