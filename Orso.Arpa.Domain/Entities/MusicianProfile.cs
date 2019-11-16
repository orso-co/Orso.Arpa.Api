using System;

namespace Orso.Arpa.Domain.Entities
{
    public class MusicianProfile : BaseEntity
    {
        internal MusicianProfile(Guid? id) : base(id)
        {
        }

        private MusicianProfile()
        {
        }

        public bool IsProfessional { get; private set; }
        public Guid PersonId { get; private set; }
        public virtual Person Person { get; private set; }
        public Guid RegisterId { get; private set; }
        public virtual Register Register { get; private set; }
    }
}
