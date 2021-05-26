using System;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfileSection : BaseEntity
    {
        public MusicianProfileSection(Guid? id, MusicianProfile musicianProfile, Section section) : base(id)
        {
            MusicianProfile = musicianProfile;
            Section = section;
        }

        public MusicianProfileSection(Guid musicianProfileId, Guid sectionId)
        {
            MusicianProfileId = musicianProfileId;
            SectionId = sectionId;
        }

        public MusicianProfileSection()
        {
        }

        public MusicianProfileSection(Create.DoublingInstrumentCommand command, Guid? id = null) : base(id)
        {
            LevelAssessmentPerformer = command.LevelAssessmentPerformer;
            LevelAssessmentStaff = command.LevelAssessmentStaff;
            InstrumentAvailabilityId = command.AvailabilityId;
            Comment = command.Comment;
            SectionId = command.InstrumentId;
        }

        public byte LevelAssessmentPerformer { get; private set; }
        public byte LevelAssessmentStaff { get; private set; }
        public Guid? InstrumentAvailabilityId { get; private set; }
        public virtual SelectValueMapping InstrumentAvailability { get; private set; }
        public string Comment { get; private set; }

        public Guid SectionId { get; private set; }
        public virtual Section Section { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}
