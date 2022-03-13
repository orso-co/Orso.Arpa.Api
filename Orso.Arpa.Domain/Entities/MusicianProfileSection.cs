using System;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfileSection : BaseEntity
    {
        protected MusicianProfileSection()
        {
        }

        public MusicianProfileSection(Guid? id, Logic.MusicianProfileSections.Create.Command command) : base(id)
        {
            LevelAssessmentInner = command.LevelAssessmentInner;
            LevelAssessmentTeam = command.LevelAssessmentTeam;
            InstrumentAvailabilityId = command.AvailabilityId;
            Comment = command.Comment;
            SectionId = command.InstrumentId;
            MusicianProfileId = command.MusicianProfileId;
        }

        public void Update(ModifyDoublingInstrument.Command command)
        {
            Comment = command.Comment;
            InstrumentAvailabilityId = command.AvailabilityId;
            LevelAssessmentInner = command.LevelAssessmentInner;
        }

        public void Update(Logic.MusicianProfileSections.Modify.Command command)
        {
            Comment = command.Comment;
            InstrumentAvailabilityId = command.AvailabilityId;
            LevelAssessmentInner = command.LevelAssessmentInner;
            LevelAssessmentTeam = command.LevelAssessmentTeam;
        }

        public byte LevelAssessmentInner { get; private set; }
        public byte LevelAssessmentTeam { get; private set; }
        public Guid? InstrumentAvailabilityId { get; private set; }
        public virtual SelectValueMapping InstrumentAvailability { get; private set; }
        public string Comment { get; private set; }

        public Guid SectionId { get; private set; }
        public virtual Section Section { get; private set; }
        public Guid MusicianProfileId { get; private set; }
        public virtual MusicianProfile MusicianProfile { get; private set; }
    }
}
