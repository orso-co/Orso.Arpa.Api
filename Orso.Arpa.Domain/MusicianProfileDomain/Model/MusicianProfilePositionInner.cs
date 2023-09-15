using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Model
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
