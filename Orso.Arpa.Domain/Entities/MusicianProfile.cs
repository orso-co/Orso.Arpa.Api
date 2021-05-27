using System;
using System.Collections.Generic;
using System.Linq;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfile : BaseEntity
    {
        public MusicianProfile(Create.Command command, bool isMainProfile, Guid? id = null) : base(id)
        {
            LevelAssessmentPerformer = command.LevelAssessmentPerformer;
            LevelAssessmentStaff = command.LevelAssessmentStaff;
            PersonId = command.PersonId;
            InstrumentId = command.InstrumentId;
            QualificationId = command.QualificationId;
            InquiryStatusPerformerId = command.InquiryStatusPerformerId;
            InquiryStatusStaffId = command.InquiryStatusStaffId;
            IsMainProfile = isMainProfile;
            DoublingInstruments = command.DoublingInstruments.Select(i => new MusicianProfileSection(i)).ToList();
            PreferredPositionsPerformer = command.PreferredPositionsPerformerIds.Select(i => new MusicianProfilePositionPerformer(i, Id)).ToList();
            PreferredPositionsStaff = command.PreferredPositionsStaffIds.Select(i => new MusicianProfilePositionStaff(i, Id)).ToList();
            //PreferredPartsPerformer = command.PreferredPartsPerformer;
            //PreferredPartsStaff = command.PreferredPartsStaff;
        }

        public MusicianProfile()
        {
        }

        #region Native
        public bool IsMainProfile { get; private set; }
        public bool IsDeactivated { get; private set; }

        public byte LevelAssessmentPerformer { get; private set; }
        public byte LevelAssessmentStaff { get; private set; }
        public byte ProfilePreferencePerformer { get; private set; }
        public byte ProfilePreferenceStaff { get; private set; }

        public string BackgroundPerformer { get; private set; }
        public string BackgroundStaff { get; private set; }
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
        public virtual ICollection<MusicianProfilePositionPerformer> PreferredPositionsPerformer { get; private set; } = new HashSet<MusicianProfilePositionPerformer>();
        public virtual ICollection<MusicianProfilePositionStaff> PreferredPositionsStaff { get; private set; } = new HashSet<MusicianProfilePositionStaff>();
        //public virtual ICollection<PreferredPart> PreferredPartsPerformer { get; private set; } = new HashSet<PreferredPart>();
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
