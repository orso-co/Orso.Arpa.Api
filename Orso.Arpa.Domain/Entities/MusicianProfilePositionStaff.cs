using System;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfilePositionStaff : BaseEntity
    {
        public MusicianProfilePositionStaff(Guid selectValueSectionId, Guid musicianProfileId, Guid? id = null) : base(id)
        {
            SelectValueSectionId = selectValueSectionId;
            MusicianProfileId = musicianProfileId;
        }

        protected MusicianProfilePositionStaff()
        {

        }

        public Guid SelectValueSectionId { get; set; }
        public virtual SelectValueSection SelectValueSection { get; set; }
        public Guid MusicianProfileId { get; set; }
        public virtual MusicianProfile MusicianProfile { get; set; }
    }
}
