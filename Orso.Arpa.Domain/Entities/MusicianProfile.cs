using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.MusicianProfiles;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfile : BaseEntity
    {
        public MusicianProfile(Guid? id, Create.Command command) : base(id)
        {
            IsProfessional = command.IsProfessional;
            PersonId = command.PersonId;
            SectionId = command.SectionId;
        }

        protected MusicianProfile()
        {
        }

        public bool IsProfessional { get; private set; }
        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }
        public Guid SectionId { get; private set; }
        public virtual Section Section { get; private set; }
        public virtual ICollection<ProjectParticipation> ProjectParticipations { get; private set; } = new HashSet<ProjectParticipation>();
    }
}
