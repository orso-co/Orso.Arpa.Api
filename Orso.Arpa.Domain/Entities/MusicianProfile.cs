using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfile : BaseEntity
    {
        internal MusicianProfile(Guid? id) : base(id)
        {
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
