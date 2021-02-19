using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Orso.Arpa.Domain.Logic.Auth;

namespace Orso.Arpa.Domain.Entities
{
    public class Person : BaseEntity
    {
        public Person(Guid? id, UserRegister.Command command) : base(id)
        {
            GivenName = command.GivenName;
            Surname = command.Surname;
        }

        [JsonConstructor]
        protected Person()
        {
        }

        [JsonProperty]
        public string GivenName { get; private set; }

        [JsonProperty]
        public string Surname { get; private set; }

        public virtual User User { get; private set; }
        public virtual ICollection<PersonAddress> Addresses { get; private set; } = new HashSet<PersonAddress>();
        public virtual ICollection<AppointmentParticipation> AppointmentParticipations { get; private set; } = new HashSet<AppointmentParticipation>();
        public virtual ICollection<MusicianProfile> MusicianProfiles { get; private set; } = new HashSet<MusicianProfile>();

        public virtual ICollection<PersonSection> StakeholderGroups { get; private set; } = new HashSet<PersonSection>();
    }
}
