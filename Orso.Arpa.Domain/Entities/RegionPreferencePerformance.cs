using System;

namespace Orso.Arpa.Domain.Entities
{
    public class RegionPreferencePerformance : BaseEntity
    {
        public RegionPreferencePerformance(Guid? id, MusicianProfile musicianProfile, Venue venue) : base(id)
        {
            MusicianProfile = musicianProfile;
            Venue = venue;
        }

        public RegionPreferencePerformance(Guid musicianProfileId, Guid venueId)
        {
            MusicianProfileId = musicianProfileId;
            VenueId = venueId;
        }

        public RegionPreferencePerformance()
        {
        }

        public Guid VenueId { get; private set; }
        public virtual Venue Venue { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
        public byte Rating { get; private set; }
    }
}
