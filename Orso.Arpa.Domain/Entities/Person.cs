using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
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

        /// <summary>
        /// This constructor is used for serialization purposes only
        /// </summary>
        /// <see cref="https://docs.microsoft.com/en-gb/dotnet/core/compatibility/serialization/5.0/non-public-parameterless-constructors-not-used-for-deserialization"/>
        public Person()
        {
        }

        [JsonInclude]
        public string GivenName { get; private set; }

        [JsonInclude]
        public string Surname { get; private set; }

        [JsonInclude]
        public virtual User User { get; private set; }

        [JsonInclude]
        public string AboutMe { get; private set; }

        [JsonInclude]
        public byte Reliability { get; private set; }

        [JsonInclude]
        public byte Favorization { get; private set; }

        [JsonInclude]
        public virtual ICollection<PersonAddress> Addresses { get; private set; } = new HashSet<PersonAddress>();

        [JsonInclude]
        public virtual ICollection<AppointmentParticipation> AppointmentParticipations { get; private set; } = new HashSet<AppointmentParticipation>();

        [JsonInclude]
        public virtual ICollection<MusicianProfile> MusicianProfiles { get; private set; } = new HashSet<MusicianProfile>();

        [JsonInclude]
        public virtual ICollection<PersonSection> StakeholderGroups { get; private set; } = new HashSet<PersonSection>();
    }
}
