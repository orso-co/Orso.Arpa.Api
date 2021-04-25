using System;

namespace Orso.Arpa.Domain.Entities
{
    public class RegionPreferenceRehearsal : BaseEntity
    {
        public RegionPreferenceRehearsal(Guid? id, MusicianProfile musicianProfile, Venue venue) : base(id)
        {
            MusicianProfile = musicianProfile;
            Venue = venue;
        }

        public RegionPreferenceRehearsal(Guid musicianProfileId, Guid venueId)
        {
            MusicianProfileId = musicianProfileId;
            VenueId = venueId;
        }

        public RegionPreferenceRehearsal()
        {
        }

        public Guid VenueId { get; private set; }
        public virtual Venue Venue { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
        public byte Rating { get; private set; }
    }
}
