using System;
using System.Collections.Generic;
using System.Linq;
using Orso.Arpa.Domain.Attributes;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Me;
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
            InquiryStatusInner = command.InquiryStatusInner;
            InquiryStatusTeam = command.InquiryStatusTeam;
            IsMainProfile = isMainProfile;
            PreferredPositionsInner = command.PreferredPositionsInnerIds.Distinct().Select(i => new MusicianProfilePositionInner(i, Id)).ToList();
            PreferredPositionsTeam = command.PreferredPositionsTeamIds.Distinct().Select(i => new MusicianProfilePositionTeam(i, Id)).ToList();
            PreferredPartsInner = command.PreferredPartsInner.Distinct().ToArray();
            PreferredPartsTeam = command.PreferredPartsTeam.Distinct().ToArray();
        }

        public MusicianProfile()
        {
        }

        public void Update(ModifyMusicianProfile.Command command)
        {
            IsMainProfile = command.IsMainProfile;
            LevelAssessmentInner = command.LevelAssessmentInner;
            ProfilePreferenceInner = command.ProfilePreferenceInner;
            BackgroundInner = command.BackgroundInner;
            PreferredPartsInner = command.PreferredPartsInner.ToArray();
            InquiryStatusInner = command.InquiryStatusInner;
        }

        public void Update(Logic.MusicianProfiles.Modify.Command command)
        {
            IsMainProfile = command.IsMainProfile;
            LevelAssessmentInner = command.LevelAssessmentInner;
            LevelAssessmentTeam = command.LevelAssessmentTeam;
            ProfilePreferenceTeam = command.ProfilePreferenceTeam;
            ProfilePreferenceInner = command.ProfilePreferenceInner;
            BackgroundInner = command.BackgroundInner;
            BackgroundTeam = command.BackgroundTeam;
            SalaryComment = command.SalaryComment;
            QualificationId = command.QualificationId;
            SalaryId = command.SalaryId;
            InquiryStatusInner = command.InquiryStatusInner;
            InquiryStatusTeam = command.InquiryStatusTeam;
            PreferredPartsInner = command.PreferredPartsInner.ToArray();
            PreferredPartsTeam = command.PreferredPartsTeam.ToArray();
        }

        public void TurnOffIsMainProfileFlag()
        {
            IsMainProfile = false;
        }

        public override string ToString()
        {
            return $"{Person} ({Instrument}, {(Qualification is null ? "qualification unknown" : Qualification)})";
        }

        #region Native
        public bool IsMainProfile { get; private set; }
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
        public MusicianProfileInquiryStatus? InquiryStatusInner { get; private set; }
        public MusicianProfileInquiryStatus? InquiryStatusTeam { get; private set; }

        [CascadingSoftDelete]
        public virtual MusicianProfileDeactivation Deactivation { get; private set; }
        public bool IsDeactivated(DateTime date) => Deactivation != null && Deactivation.DeactivationStart.Date < date.Date;

        #endregion

        #region Collection

        [CascadingSoftDelete]
        public virtual ICollection<MusicianProfileSection> DoublingInstruments { get; private set; } = new HashSet<MusicianProfileSection>();

        [CascadingSoftDelete]
        public virtual ICollection<Education> Educations { get; private set; } = new HashSet<Education>();

        [CascadingSoftDelete]
        public virtual ICollection<CurriculumVitaeReference> CurriculumVitaeReferences { get; private set; } = new HashSet<CurriculumVitaeReference>();

        [CascadingSoftDelete]
        public virtual ICollection<MusicianProfilePositionInner> PreferredPositionsInner { get; private set; } = new HashSet<MusicianProfilePositionInner>();

        [CascadingSoftDelete]
        public virtual ICollection<MusicianProfilePositionTeam> PreferredPositionsTeam { get; private set; } = new HashSet<MusicianProfilePositionTeam>();

        [CascadingSoftDelete]
        public virtual ICollection<PreferredGenre> PreferredGenres { get; private set; } = new HashSet<PreferredGenre>();

        [CascadingSoftDelete]
        public virtual ICollection<MusicianProfileDocument> Documents { get; private set; } = new HashSet<MusicianProfileDocument>();

        [CascadingSoftDelete]
        public virtual ICollection<RegionPreference> RegionPreferences { get; private set; } = new HashSet<RegionPreference>();

        [CascadingSoftDelete]
        public virtual ICollection<ProjectParticipation> ProjectParticipations { get; private set; } = new HashSet<ProjectParticipation>();

        #endregion
    }
}
