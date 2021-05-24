using System;

namespace Orso.Arpa.Domain.Entities
{
    public class PreferredPart : BaseEntity
    {
        public PreferredPart(Guid? id, MusicianProfile musicianProfile, InstrumentPart part) : base(id)
        {
            MusicianProfile = musicianProfile;
            Part = part;
        }

        public PreferredPart(Guid musicianProfileId, Guid positionId)
        {
            MusicianProfileId = musicianProfileId;
            PartId = positionId;
        }

        public PreferredPart() { }


        public Guid? PartId { get; private set; }
        public virtual InstrumentPart Part { get; private set; }

        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}
