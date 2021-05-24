using System;

namespace Orso.Arpa.Domain.Entities
{
    public class PreferredPosition : BaseEntity
    {
        public PreferredPosition(Guid? id, MusicianProfile musicianProfile, Position position) : base(id)
        {
            MusicianProfile = musicianProfile;
            Position = position;
        }

        public PreferredPosition(Guid musicianProfileId, Guid positionId)
        {
            MusicianProfileId = musicianProfileId;
            PositionId = positionId;
        }

        public PreferredPosition() { }

        public Guid? PositionId { get; private set; }
        public virtual Position Position { get; private set; }

        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}
