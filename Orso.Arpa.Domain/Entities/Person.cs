using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.Attributes;
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
            PersonBackgroundTeam = command.PersonBackgroundTeam;
            GenderId = command.GenderId;
            ContactViaId = command.ContactViaId;
            ExperienceLevel = command.ExperienceLevel;
            Reliability = command.Reliability;
            GeneralPreference = command.GeneralPreference;
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

        public void Update(UserRegister.Command command)
        {
            GivenName = command.GivenName;
            Surname = command.Surname;
            DateOfBirth = command.DateOfBirth > DateTime.MinValue ? command.DateOfBirth : DateOfBirth;
            GenderId = command.GenderId;
        }

        public void Update(Logic.Me.Modify.Command command)
        {
            GivenName = command.GivenName;
            Surname = command.Surname;
            AboutMe = command.AboutMe;
            GenderId = command.GenderId;
            DateOfBirth = command.DateOfBirth;
            Birthplace = command.Birthplace;
            BirthName = command.BirthName;
        }

        public void Update(Modify.Command command)
        {
            GivenName = command.GivenName;
            Surname = command.Surname;
            BirthName = command.BirthName;
            AboutMe = command.AboutMe;
            GenderId = command.GenderId;
            DateOfBirth = command.DateOfBirth;
            Birthplace = command.Birthplace;
            PersonBackgroundTeam = command.PersonBackgroundTeam;
            ContactViaId = command.ContactViaId;
            ExperienceLevel = command.ExperienceLevel;
            Reliability = command.Reliability;
            GeneralPreference = command.GeneralPreference;
        }

        public void SetProfilePitureName(string profilePitureName)
        {
            ProfilePictureFileName = profilePitureName;
        }

        public string GetPreferredEMailAddress()
        {
            return User?.Email ??
                    ContactDetails
                        .Where(cd => cd.Key == Enums.ContactDetailKey.EMail)
                        .OrderByDescending(cd => cd.Preference)
                        .FirstOrDefault()?.Value;
        }

        public override string ToString()
        {
            return DisplayName;
        }

        internal void ClearContactVia()
        {
            ContactViaId = null;
        }

        [JsonInclude]
        public string GivenName { get; private set; }

        [JsonInclude]
        public string Surname { get; private set; }

        public string BirthName { get; private set; }

        [JsonInclude]
        public string AboutMe { get; private set; }

        public string PersonBackgroundTeam { get; private set; }

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

        public string ProfilePictureFileName { get; private set; }

        /// <summary>
        /// Contains data from podio and filemaker migration in json format
        /// </summary>
        public string MovingBox { get; private set; }

        [JsonInclude]
        public virtual User User { get; private set; }

        public Guid? GenderId { get; private set; }

        public virtual SelectValueMapping Gender { get; private set; }

        [CascadingSoftDelete]
        [JsonInclude]
        public virtual ICollection<Address> Addresses { get; private set; } = new HashSet<Address>();

        [CascadingSoftDelete]
        public virtual ICollection<ContactDetail> ContactDetails { get; private set; } = new HashSet<ContactDetail>();

        [JsonInclude]
        [CascadingSoftDelete]
        public virtual ICollection<AppointmentParticipation> AppointmentParticipations { get; private set; } = new HashSet<AppointmentParticipation>();

        [JsonInclude]
        [CascadingSoftDelete]
        public virtual ICollection<MusicianProfile> MusicianProfiles { get; private set; } = new HashSet<MusicianProfile>();

        [JsonInclude]
        [CascadingSoftDelete]
        public virtual ICollection<PersonSection> StakeholderGroups { get; private set; } = new HashSet<PersonSection>();

        [CascadingSoftDelete]
        public virtual ICollection<BankAccount> BankAccounts { get; private set; } = new HashSet<BankAccount>();

        public string DisplayName => !string.IsNullOrEmpty(GivenName) && !string.IsNullOrEmpty(Surname) ? $"{GivenName} {Surname}" : (GivenName ?? Surname);

    }
}
