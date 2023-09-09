using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.SelectValueDomain.Model;

namespace Orso.Arpa.Domain.MusicianProfileDomain.Model
{
    public class PreferredGenre : BaseEntity
    {
        public PreferredGenre(Guid? id, MusicianProfile musicianProfile, SelectValueMapping selectValueMapping) : base(id)
        {
            MusicianProfile = musicianProfile;
            SelectValueMapping = selectValueMapping;
        }

        public PreferredGenre(Guid musicianProfileId, Guid selectValueMappingId)
        {
            MusicianProfileId = musicianProfileId;
            SelectValueMappingId = selectValueMappingId;
        }

        public PreferredGenre() { }


        public Guid SelectValueMappingId { get; private set; }
        public virtual SelectValueMapping SelectValueMapping { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}
