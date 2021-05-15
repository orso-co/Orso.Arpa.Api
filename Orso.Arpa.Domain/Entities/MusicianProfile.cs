using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfile : BaseEntity
    {
        public MusicianProfile(Guid? id, Create.Command command) : base(id)
        {
            #region Native
            LevelAssessmentPerformer = command.LevelAssessmentPerformer;
            LevelAssessmentStaff = command.LevelAssessmentStaff;
            #endregion

            #region Reference
            PersonId = command.PersonId;
            InstrumentId = command.InstrumentId;
            QualificationId = command.QualificationId;
            InquiryStatusPerformerId = command.InquiryStatusPerformerId;
            InquiryStatusStaffId = command.InquiryStatusStaffId;
            #endregion

            #region Collection
            DoublingInstruments = command.DoublingInstruments;
            //PreferredPositionsPerformer = command.PreferredPositionsPerformer;
            //PreferredPositionsStaff = command.PreferredPositionsStaff;
            //PreferredPartsPerformer = command.PreferredPartsPerformer;
            //PreferredPartsStaff = command.PreferredPartsStaff;
            #endregion
        }

        public MusicianProfile()
        {
        }

        #region Native
        public bool IsMainProfile { get; set; }
        public bool IsDeactivated { get; set; }

        public byte LevelAssessmentPerformer { get; private set; }
        public byte LevelAssessmentStaff { get; private set; }
        public byte ProfilePreferencePerformer { get; set; }
        public byte ProfilePreferenceStaff { get; set; }

        public string BackgroundPerformer { get; set; }
        public string BackgroundStaff { get; set; }
        public string SalaryComment { get; set; }
        #endregion

        #region Reference
        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }

        public Guid InstrumentId { get; private set; }
        public virtual Section Instrument { get; private set; }

        public Guid? QualificationId { get; private set; }
        public virtual SelectValueMapping Qualification { get; private set; }

        public Guid? SalaryId { get; set; }
        public virtual SelectValueMapping Salary { get; private set; }

        public Guid? InquiryStatusPerformerId { get; private set; }
        public virtual SelectValueMapping InquiryStatusPerformer { get; private set; }

        public Guid? InquiryStatusStaffId { get; private set; }
        public virtual SelectValueMapping InquiryStatusStaff { get; private set; }
        #endregion

        #region Collection
        public virtual ICollection<MusicianProfileSection> DoublingInstruments { get; private set; } = new HashSet<MusicianProfileSection>();
        public virtual ICollection<MusicianProfileEducation> MusicianProfileEducations { get; private set; } = new HashSet<MusicianProfileEducation>();
        public virtual ICollection<PreferredPosition> PreferredPositionsPerformer { get; private set; } = new HashSet<PreferredPosition>();
        //public virtual ICollection<PreferredPosition> PreferredPositionsStaff { get; private set; } = new HashSet<PreferredPosition>();
        public virtual ICollection<PreferredPart> PreferredPartsPerformer { get; private set; } = new HashSet<PreferredPart>();
        //public virtual ICollection<PreferredPart> PreferredPartsStaff { get; private set; } = new HashSet<PreferredPart>();

        //Todo: ARPA-325
        public virtual ICollection<MusicianProfileCurriculumVitaeReference> MusicianProfileCurriculumVitaeReferences { get; private set; } = new HashSet<MusicianProfileCurriculumVitaeReference>();

        //Todo: ARPA-326
        public virtual ICollection<PreferredGenre> PreferredGenres { get; private set; } = new HashSet<PreferredGenre>();

        //Todo: ARPA-329
        public virtual ICollection<AvailableDocument> AvailableDocuments { get; private set; } = new HashSet<AvailableDocument>();

        //Todo: ARPA-327
        public virtual ICollection<RegionPreferencePerformance> RegionPreferencePerformances { get; private set; } = new HashSet<RegionPreferencePerformance>();

        //Todo: ARPA-327
        public virtual ICollection<RegionPreferenceRehearsal> RegionPreferenceRehearsals { get; private set; } = new HashSet<RegionPreferenceRehearsal>();

        //Todo: ARPA-328
        public virtual ICollection<Audition> Auditions { get; private set; } = new HashSet<Audition>();
        #endregion

        #region cross reference
        public virtual ICollection<ProjectParticipation> ProjectParticipations { get; private set; } = new HashSet<ProjectParticipation>();
        #endregion
    }
}
