using System;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfileCurriculumVitaeReference : BaseEntity
    {
        public MusicianProfileCurriculumVitaeReference(Guid? id, MusicianProfile musicianProfile, CurriculumVitaeReference curriculumVitaeReference) : base(id)
        {
            MusicianProfile = musicianProfile;
            CurriculumVitaeReference = curriculumVitaeReference;
        }

        public MusicianProfileCurriculumVitaeReference(Guid musicianProfileId, Guid curriculumVitaeReferenceId)
        {
            MusicianProfileId = musicianProfileId;
            CurriculumVitaeReferenceId = curriculumVitaeReferenceId;
        }

        public MusicianProfileCurriculumVitaeReference()
        {
        }

        public Guid CurriculumVitaeReferenceId { get; private set; }
        public virtual CurriculumVitaeReference CurriculumVitaeReference { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}
