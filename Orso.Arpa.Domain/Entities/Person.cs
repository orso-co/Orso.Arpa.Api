using System;
using System.Collections.Generic;

namespace Orso.Arpa.Domain.Entities
{
    public class Person : BaseEntity
    {
        public Person(Guid id, Auth.Register.Command command) : base(id)
        {
            GivenName = command.GivenName;
            Surname = command.Surname;
        }

        public Person(Guid id) : base(id)
        {
        }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }
        public virtual User user { get; private set; }
        public virtual ICollection<PersonAddress> Addresses { get; private set; } = new HashSet<PersonAddress>();
        public virtual ICollection<AppointmentParticipation> AppointmentParticipations { get; private set; } = new HashSet<AppointmentParticipation>();
        public virtual ICollection<MusicianProfile> MusicianProfiles { get; private set; } = new HashSet<MusicianProfile>();
    }
}
