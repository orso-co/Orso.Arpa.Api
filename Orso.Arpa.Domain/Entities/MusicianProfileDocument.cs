using System;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfileDocument : BaseEntity
    {
        public MusicianProfileDocument(Guid? id, MusicianProfile musicianProfile, SelectValueMapping selectValueMapping) : base(id)
        {
            MusicianProfile = musicianProfile;
            SelectValueMapping = selectValueMapping;
        }

        public MusicianProfileDocument(Guid musicianProfileId, Guid selectValueMappingId, Guid? id = null) : base(id)
        {
            MusicianProfileId = musicianProfileId;
            SelectValueMappingId = selectValueMappingId;
        }

        public MusicianProfileDocument()
        {
        }

        public Guid SelectValueMappingId { get; private set; }
        public virtual SelectValueMapping SelectValueMapping { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}
