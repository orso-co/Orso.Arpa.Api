using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfile : BaseEntity
    {
        public MusicianProfile(Guid? id, Create.Command command) : base(id)
        {
            PersonId = command.PersonId;
            InstrumentId = command.InstrumentId;
        }

        public MusicianProfile()
        {
        }

        #region Native
        public byte LevelAssessmentPerformer { get; private set; }
        public byte LevelAssessmentStaff { get; private set; }
        public byte ProfilePreferencePerformer { get; private set; }
        public byte ProfilePreferenceStaff { get; private set; }
        public bool IsMainProfile { get; private set; }
        public string Background { get; private set; }
        public byte ExperienceLevel { get; private set; }
        public string SalaryComment { get; private set; }
        #endregion

        #region Reference
        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }

        public Guid InstrumentId { get; private set; }
        public virtual Section Instrument { get; private set; }

        public Guid? QualificationId { get; private set; }
        public virtual SelectValueMapping Qualification { get; private set; }

        public Guid? SalaryId { get; private set; }
        public virtual SelectValueMapping Salary { get; private set; }

        public Guid? InquiryStatusPerformerId { get; private set; }
        public virtual SelectValueMapping InquiryStatusPerformer { get; private set; }

        public Guid? InquiryStatusStaffId { get; private set; }
        public virtual SelectValueMapping InquiryStatusStaff { get; private set; }
        #endregion

        #region Collection
        public virtual ICollection<MusicianProfileSection> DoublingInstruments { get; private set; } = new HashSet<MusicianProfileSection>();

        public virtual ICollection<MusicianProfileEducation> MusicianProfileEducations { get; private set; } = new HashSet<MusicianProfileEducation>();

        public virtual ICollection<MusicianProfileCurriculumVitaeReference> MusicianProfileCurriculumVitaeReferences { get; private set; } = new HashSet<MusicianProfileCurriculumVitaeReference>();

        public virtual ICollection<PreferredGenre> PreferredGenres { get; private set; } = new HashSet<PreferredGenre>();

        public virtual ICollection<PreferredPosition> PreferredPositions { get; private set; } = new HashSet<PreferredPosition>();

        public virtual ICollection<AvailableDocument> AvailableDocuments { get; private set; } = new HashSet<AvailableDocument>();

        public virtual ICollection<RegionPreferencePerformance> RegionPreferencePerformances { get; private set; } = new HashSet<RegionPreferencePerformance>();

        public virtual ICollection<RegionPreferenceRehearsal> RegionPreferenceRehearsals { get; private set; } = new HashSet<RegionPreferenceRehearsal>();

        public virtual ICollection<Audition> Auditions { get; private set; } = new HashSet<Audition>();
        #endregion

        #region cross reference
        public virtual ICollection<ProjectParticipation> ProjectParticipations { get; private set; } = new HashSet<ProjectParticipation>();
        #endregion
    }
}
