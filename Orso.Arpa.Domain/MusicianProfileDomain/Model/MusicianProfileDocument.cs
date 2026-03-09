using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Model
{
    public class MusicianProfileDocument : BaseEntity
    {
        public MusicianProfileDocument(Guid? id, MusicianProfile musicianProfile, SelectValueMapping selectValueMapping) : base(id ?? Guid.NewGuid())
        {
            MusicianProfile = musicianProfile;
            SelectValueMapping = selectValueMapping;
        }

        public MusicianProfileDocument(Guid musicianProfileId, Guid selectValueMappingId, Guid? id = null) : base(id ?? Guid.NewGuid())
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
