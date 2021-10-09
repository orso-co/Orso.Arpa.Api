using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Domain.Logic.Persons;

namespace Orso.Arpa.Domain.Entities
{
    public class Person : BaseEntity
    {
        public Person(Guid? id, Create.Command command) : base(id)
        {
            GivenName = command.GivenName;
            Surname = command.Surname;
            AboutMe = command.AboutMe;
            BirthName = command.BirthName;
            DateOfBirth = command.DateOfBirth;
            Birthplace = command.Birthplace;
            GenderId = command.GenderId;
            ContactViaId = command.ContactViaId;
            ExperienceLevel = command.ExperienceLevel;
            Reliability = command.Reliability;
        }

        public Person(Guid? id, UserRegister.Command command) : base(id)
        {
            GivenName = command.GivenName;
            Surname = command.Surname;
            GenderId = command.GenderId;
            DateOfBirth = command.DateOfBirth;
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

        public string BirthName { get; private set; }

        [JsonInclude]
        public string AboutMe { get; private set; }

        public Guid? ContactViaId { get; private set; }
        public virtual Person ContactVia { get; private set; }
        public virtual ICollection<Person> ContactsRecommended { get; private set; } = new HashSet<Person>();

        [JsonInclude]
        public byte Reliability { get; private set; }

        [JsonInclude]
        public byte GeneralPreference { get; private set; }

        public DateTime? DateOfBirth { get; private set; }

        public string Birthplace { get; private set; }

        public byte ExperienceLevel { get; private set; }

        /// <summary>
        /// Contains data from podio and filemaker migration in json format
        /// </summary>
        public string MovingBox { get; private set; }

        [JsonInclude]
        public virtual User User { get; private set; }

        public Guid? GenderId { get; private set; }

        public virtual SelectValueMapping Gender { get; private set; }

        [JsonInclude]
        public virtual ICollection<Address> Addresses { get; private set; } = new HashSet<Address>();

        public virtual ICollection<ContactDetail> ContactDetails { get; private set; } = new HashSet<ContactDetail>();

        [JsonInclude]
        public virtual ICollection<AppointmentParticipation> AppointmentParticipations { get; private set; } = new HashSet<AppointmentParticipation>();

        [JsonInclude]
        public virtual ICollection<MusicianProfile> MusicianProfiles { get; private set; } = new HashSet<MusicianProfile>();

        [JsonInclude]
        public virtual ICollection<PersonSection> StakeholderGroups { get; private set; } = new HashSet<PersonSection>();
        public virtual ICollection<BankAccount> BankAccounts { get; private set; } = new HashSet<BankAccount>();

    }
}
