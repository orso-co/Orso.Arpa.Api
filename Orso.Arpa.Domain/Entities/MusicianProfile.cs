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

        public byte LevelSelfAssessment { get; private set; }
        public byte LevelInnerASsessment { get; private set; }
        public byte ProfileFavorizitation { get; private set; }
        public bool IsMainProfile { get; private set; }
        public string Background { get; private set; }
        public byte ExperienceLevel { get; private set; }

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

        public Guid? InqueryId { get; private set; }
        public virtual SelectValueMapping Inquery { get; private set; }

        public Guid? PreferredPositionId { get; private set; }
        public virtual Position PreferredPosition { get; private set; }

        #endregion
        #region Collection

        public virtual ICollection<ProjectParticipation> ProjectParticipations { get; private set; } = new HashSet<ProjectParticipation>();

        public virtual ICollection<MusicianProfileSection> DoublingInstruments { get; private set; } = new HashSet<MusicianProfileSection>();

        public virtual ICollection<MusicianProfileEducation> MusicianProfileEducations { get; private set; } = new HashSet<MusicianProfileEducation>();

        public virtual ICollection<MusicianProfileCredential> MusicianProfileCredentials { get; private set; } = new HashSet<MusicianProfileCredential>();

        public virtual ICollection<MusicianProfileSelectValueMapping> PreferredGenres { get; private set; } = new HashSet<MusicianProfileSelectValueMapping>();

        #endregion
    }
}
