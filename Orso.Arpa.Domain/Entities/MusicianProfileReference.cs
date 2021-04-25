using System;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfileReference : BaseEntity
    {
        public MusicianProfileReference(Guid? id, MusicianProfile musicianProfile, Reference reference) : base(id)
        {
            MusicianProfile = musicianProfile;
            Reference = reference;
        }

        public MusicianProfileReference(Guid musicianProfileId, Guid referenceId)
        {
            MusicianProfileId = musicianProfileId;
            ReferenceId = referenceId;
        }

        public MusicianProfileReference()
        {
        }

        public Guid ReferenceId { get; private set; }
        public virtual Reference Reference { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}
