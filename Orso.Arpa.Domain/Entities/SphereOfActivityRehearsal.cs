using System;

namespace Orso.Arpa.Domain.Entities
{
    public class SphereOfActivityRehearsal : BaseEntity
    {
        public SphereOfActivityRehearsal(Guid? id, MusicianProfile musicianProfile, Venue venue) : base(id)
        {
            MusicianProfile = musicianProfile;
            Venue = venue;
        }

        public SphereOfActivityRehearsal(Guid musicianProfileId, Guid venueId)
        {
            MusicianProfileId = musicianProfileId;
            VenueId = venueId;
        }

        public SphereOfActivityRehearsal()
        {
        }

        public Guid VenueId { get; private set; }
        public virtual Venue Venue { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
        public byte Rating { get; private set; }
    }
}
