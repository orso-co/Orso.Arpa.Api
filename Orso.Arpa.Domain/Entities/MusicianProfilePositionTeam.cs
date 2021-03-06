using System;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfilePositionTeam : BaseEntity
    {
        public MusicianProfilePositionTeam(Guid selectValueSectionId, Guid musicianProfileId, Guid? id = null) : base(id)
        {
            SelectValueSectionId = selectValueSectionId;
            MusicianProfileId = musicianProfileId;
        }

        protected MusicianProfilePositionTeam()
        {

        }

        public Guid SelectValueSectionId { get; set; }
        public virtual SelectValueSection SelectValueSection { get; set; }
        public Guid MusicianProfileId { get; set; }
        public virtual MusicianProfile MusicianProfile { get; set; }
    }
}
