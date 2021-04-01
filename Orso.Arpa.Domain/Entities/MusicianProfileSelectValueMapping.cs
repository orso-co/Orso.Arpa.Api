using System;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfileSelectValueMapping : BaseEntity
    {
        public MusicianProfileSelectValueMapping(Guid? id, MusicianProfile musicianProfile, SelectValueMapping selectValueMapping) : base(id)
        {
            MusicianProfile = musicianProfile;
            SelectValueMapping = selectValueMapping;
        }

        public MusicianProfileSelectValueMapping(Guid musicianProfileId, Guid selectValueMappingId)
        {
            MusicianProfileId = musicianProfileId;
            SelectValueMappingId = selectValueMappingId;
        }

        public MusicianProfileSelectValueMapping()
        {
        }

        public Guid SelectValueMappingId { get; private set; }
        public virtual SelectValueMapping SelectValueMapping { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}
