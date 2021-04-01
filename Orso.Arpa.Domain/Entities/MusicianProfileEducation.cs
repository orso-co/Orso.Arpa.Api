using System;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfileEducation : BaseEntity
    {
        public MusicianProfileEducation(Guid? id, MusicianProfile musicianProfile, Education education) : base(id)
        {
            MusicianProfile = musicianProfile;
            Education = education;
        }

        public MusicianProfileEducation(Guid musicianProfileId, Guid educationId)
        {
            MusicianProfileId = musicianProfileId;
            EducationId = educationId;
        }

        public MusicianProfileEducation()
        {
        }

        public Guid EducationId { get; private set; }
        public virtual Education Education { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}
