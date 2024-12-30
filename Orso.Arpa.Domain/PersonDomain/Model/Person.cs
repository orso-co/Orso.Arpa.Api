using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Orso.Arpa.Domain.AddressDomain.Model;
using Orso.Arpa.Domain.AppointmentDomain.Model;
using Orso.Arpa.Domain.ClubDomain.Model;
using Orso.Arpa.Domain.General.Attributes;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.SelectValueDomain.Model;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Model;

namespace Orso.Arpa.Domain.PersonDomain.Model
{
    public class Person : BaseEntity
    {
        public Person(Guid id, CreatePerson.Command command) : base(id)
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

        public Person(Guid id, RegisterUser.Command command) : base(id)
        {
            GivenName = command.GivenName;
            Surname = command.Surname;
            GenderId = command.GenderId;
            DateOfBirth = command.DateOfBirth;
            AboutMe = command.AboutMe;
        }

        /// <summary>
        /// This constructor is used for serialization purposes only
        /// </summary>
        /// <see cref="https://docs.microsoft.com/en-gb/dotnet/core/compatibility/serialization/5.0/non-public-parameterless-constructors-not-used-for-deserialization"/>
        public Person()
        {
        }

        public void Update(RegisterUser.Command command)
        {
            GivenName = command.GivenName;
            Surname = command.Surname;
            DateOfBirth = command.DateOfBirth > DateTime.MinValue ? command.DateOfBirth : DateOfBirth;
            GenderId = command.GenderId;
            AboutMe = command.AboutMe;
        }

        public void Update(ModifyMyUser.Command command)
        {
            GivenName = command.GivenName;
            Surname = command.Surname;
            AboutMe = command.AboutMe;
            GenderId = command.GenderId;
            DateOfBirth = command.DateOfBirth;
            Birthplace = command.Birthplace;
            BirthName = command.BirthName;
        }

        public void Update(ModifyPerson.Command command)
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

        [CascadingSoftDelete]
        public virtual ICollection<ClubMembershipProfile> ClubMemberships { get; private set; } = new HashSet<ClubMembershipProfile>();

        public string DisplayName => !string.IsNullOrEmpty(GivenName) && !string.IsNullOrEmpty(Surname) ? $"{GivenName} {Surname}" : (GivenName ?? Surname);

    }
}
