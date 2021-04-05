using System;

namespace Orso.Arpa.Domain.Entities
{
    public class SphereOfActivityConcert : BaseEntity
    {
        public SphereOfActivityConcert(Guid? id, MusicianProfile musicianProfile, Venue venue) : base(id)
        {
            MusicianProfile = musicianProfile;
            Venue = venue;
        }

        public SphereOfActivityConcert(Guid musicianProfileId, Guid venueId)
        {
            MusicianProfileId = musicianProfileId;
            VenueId = venueId;
        }

        public SphereOfActivityConcert()
        {
        }

        public Guid VenueId { get; private set; }
        public virtual Venue Venue { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
        public byte Rating { get; private set; }
    }
}
