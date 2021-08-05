using System;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Domain.Entities
{
    public class RegionPreference : BaseEntity
    {
        public RegionPreference(Guid? id, CreateRegionPreference.Command command) : base(id)
        {
            RegionId = command.RegionId;
            MusicianProfileId = command.MusicianProfileId;
            Rating = command.Rating;
            Comment = command.Comment;
        }

        protected RegionPreference()
        {
        }

        public Guid RegionId { get; private set; }
        public virtual Region Region { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
        public byte Rating { get; private set; }
        public string Comment { get; private set; }
        public RegionPreferenceType Type { get; private set; }
    }
}
