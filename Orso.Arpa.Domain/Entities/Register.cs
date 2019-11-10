using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Entities
{
    public class Register : BaseEntity
    {
        public Register(Guid id) : base(id)
        {
        }

        public string Name { get; private set; }
        public Guid? ParentId { get; private set; }
        public virtual Register Parent { get; private set; }
        public virtual ICollection<Register> Children { get; private set; } = new HashSet<Register>();
        public virtual ICollection<RegisterAppointment> RegisterAppointments { get; private set; } = new HashSet<RegisterAppointment>();
        public virtual ICollection<MusicianProfile> MusicianProfiles { get; private set; } = new HashSet<MusicianProfile>();
    }
}
