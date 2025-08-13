using System;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.RegionDomain.Commands;
using Orso.Arpa.Domain.RegionDomain.Enums;

namespace Orso.Arpa.Domain.RegionDomain.Model
{
    public class RegionPreference : BaseEntity
    {
        public RegionPreference(Guid id, CreateMyRegionPreference.Command command) : base(id)
        {
            RegionId = command.RegionId;
            MusicianProfileId = command.MusicianProfileId;
            Rating = command.Rating;
            Comment = command.Comment;
            Type = command.Type;
        }

        protected RegionPreference()
        {
        }

        public void Update(ModifyMyRegionPreference.Command command)
        {
            Rating = command.Rating;
            Comment = command.Comment;
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
