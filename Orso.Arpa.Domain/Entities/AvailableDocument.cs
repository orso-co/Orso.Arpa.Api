using System;

namespace Orso.Arpa.Domain.Entities
{
    public class AvailableDocument : BaseEntity
    {
        public AvailableDocument(Guid? id, MusicianProfile musicianProfile, SelectValueMapping selectValueMapping) : base(id)
        {
            MusicianProfile = musicianProfile;
            SelectValueMapping = selectValueMapping;
        }

        public AvailableDocument(Guid musicianProfileId, Guid selectValueMappingId)
        {
            MusicianProfileId = musicianProfileId;
            SelectValueMappingId = selectValueMappingId;
        }

        public AvailableDocument()
        {
        }

        public Guid SelectValueMappingId { get; private set; }
        public virtual SelectValueMapping SelectValueMapping { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}
