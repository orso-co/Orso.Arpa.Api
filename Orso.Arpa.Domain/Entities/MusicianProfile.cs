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
            LevelAssessmentInner = command.LevelAssessmentInner;
            LevelAssessmentTeam = command.LevelAssessmentTeam;
            PersonId = command.PersonId;
            InstrumentId = command.InstrumentId;
            QualificationId = command.QualificationId;
            InquiryStatusInnerId = command.InquiryStatusInnerId;
            InquiryStatusTeamId = command.InquiryStatusTeamId;
            IsMainProfile = isMainProfile;
            PreferredPositionsInner = command.PreferredPositionsInnerIds.Distinct().Select(i => new MusicianProfilePositionInner(i, Id)).ToList();
            PreferredPositionsTeam = command.PreferredPositionsTeamIds.Distinct().Select(i => new MusicianProfilePositionTeam(i, Id)).ToList();
            PreferredPartsInner = command.PreferredPartsInner.Distinct().ToArray();
            PreferredPartsTeam = command.PreferredPartsTeam.Distinct().ToArray();
            DoublingInstruments = command.DoublingInstruments.Select(c => new MusicianProfileSection(c)).ToList();
        }

        public MusicianProfile()
        {
        }

        #region Native
        public bool IsMainProfile { get; private set; }
        public bool IsDeactivated { get; private set; }

        public byte LevelAssessmentInner { get; private set; }
        public byte LevelAssessmentTeam { get; private set; }
        public byte ProfilePreferenceInner { get; private set; }
        public byte ProfilePreferenceTeam { get; private set; }

        public string BackgroundInner { get; private set; }
        public string BackgroundTeam { get; private set; }
        public string SalaryComment { get; private set; }

        public byte[] PreferredPartsInner { get; private set; } = Array.Empty<byte>();
        public byte[] PreferredPartsTeam { get; private set; } = Array.Empty<byte>();

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

        public Guid? InquiryStatusInnerId { get; private set; }
        public virtual SelectValueMapping InquiryStatusInner { get; private set; }

        public Guid? InquiryStatusTeamId { get; private set; }
        public virtual SelectValueMapping InquiryStatusTeam { get; private set; }
        #endregion

        #region Collection
        public virtual ICollection<MusicianProfileSection> DoublingInstruments { get; private set; } = new HashSet<MusicianProfileSection>();
        public virtual ICollection<MusicianProfileEducation> MusicianProfileEducations { get; private set; } = new HashSet<MusicianProfileEducation>();
        public virtual ICollection<MusicianProfilePositionInner> PreferredPositionsInner { get; private set; } = new HashSet<MusicianProfilePositionInner>();
        public virtual ICollection<MusicianProfilePositionTeam> PreferredPositionsTeam { get; private set; } = new HashSet<MusicianProfilePositionTeam>();

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

        public void SetActiveStatus(bool active)
        {
            IsDeactivated = !active;
        }

        public void TurnOffIsMainProfileFlag()
        {
            IsMainProfile = false;
        }
    }
}
