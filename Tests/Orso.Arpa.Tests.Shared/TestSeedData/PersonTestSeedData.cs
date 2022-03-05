using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Domain.Logic.BankAccounts;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.Extensions;


namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class PersonTestSeedData
    {
        public static IList<Person> Persons
        {
            get
            {
                return new List<Person>
                {
                    Performer,
                    Staff,
                    UserWithoutRole,
                    DeletedUser,
                    LockedOutUser,
                    UnconfirmedUser,
                    PersonWithoutUser,
                    Person1WithSameEmail,
                    Person2WithSameEmail,
                    PersonWithoutEmail,
                    PersonWithMultipleEmails
                };
            }
        }

        public static Person Performer
        {
            get
            {
                var person = new Person(
                    Guid.Parse("cb441176-eecb-4c56-908d-5a6afec36a95"),
                    new UserRegister.Command { GivenName = "Per", Surname = "Former", GenderId = SelectValueMappingSeedData.PersonGenderMappings[2].Id });
                person.SetProperty(nameof(Person.Reliability), (byte)3);
                person.SetProperty(nameof(Person.GeneralPreference), (byte)2);
                person.SetProperty(nameof(Person.ExperienceLevel), (byte)5);
                person.SetProperty(nameof(Person.DateOfBirth), new DateTime(1979, 5, 5));
                person.SetProperty(nameof(Person.ContactViaId), UnconfirmedUser.Id);
                person.SetProperty(nameof(Person.Birthplace), "Pusemuckel");
                person.SetProperty(nameof(Person.BirthName), "Sil");
                person.SetProperty(nameof(Person.AboutMe), "Oh yes, I'm the great pretender!");
                person.StakeholderGroups.Add(new PersonSection(
                    Guid.Parse("e94c3925-4edb-4a56-a8a0-a655500f8f4a"),
                    person.Id,
                    SectionSeedData.Choir.Id));
                return person;
            }
        }

        public static Person Staff
        {
            get
            {
                return new Person(
                    Guid.Parse("c0c8470b-e6a0-4a0b-8a4c-24d503636248"),
                    new UserRegister.Command { GivenName = "Staff", Surname = "Member", GenderId = SelectValueMappingSeedData.PersonGenderMappings[2].Id });
            }
        }

        public static Person UserWithoutRole
        {
            get
            {
                var person = new Person(
                    Guid.Parse("32e46032-125d-463a-87ed-67d9a34154c4"),
                    new UserRegister.Command { GivenName = "Without", Surname = "Role", GenderId = SelectValueMappingSeedData.PersonGenderMappings[2].Id });
                person.Addresses.Add(new Address(Guid.Parse("df196870-5045-4a1c-b7fe-40473889830d"), new Domain.Logic.Addresses.Create.Command
                {
                    UrbanDistrict = "Altstadt-Lehel",
                    Address1 = "Viktualienmarkt 4",
                    Address2 = "3. Etage rechts",
                    City = "München",
                    CommentInner = "Zweitwohnsitz",
                    Country = "Deutschland",
                    PersonId = person.Id,
                    State = "Bayern",
                    TypeId = SelectValueMappingSeedData.AddressTypeMappings[1].Id,
                    Zip = "80331"
                }));
                var contactDetail = new ContactDetail(Guid.Parse("252335a7-58ef-431b-ad1e-cc0ca53ebeaf"), new Domain.Logic.ContactDetails.Create.Command
                {
                    PersonId = person.Id,
                    TypeId = SelectValueMappingSeedData.ContactDetailTypeMappings[0].Id,
                    Key = ContactDetailKey.EMail,
                    Value = "user@without.role"
                });
                person.ContactDetails.Add(contactDetail);
                return person;
            }
        }

        public static Person DeletedUser
        {
            get
            {
                return new Person(
                    Guid.Parse("4d98408b-620e-4ea5-9661-ab8efcad4495"),
                    new UserRegister.Command { GivenName = "Deleted", Surname = "User", GenderId = SelectValueMappingSeedData.PersonGenderMappings[2].Id });
            }
        }

        public static Person LockedOutUser
        {
            get
            {
                return new Person(
                    Guid.Parse("860e9e57-e213-4cc1-aa7c-49918d5b75dd"),
                    new UserRegister.Command { GivenName = "LockedOut", Surname = "User", GenderId = SelectValueMappingSeedData.PersonGenderMappings[2].Id });
            }
        }

        public static Person PersonWithoutUser
        {
            get
            {
                var person = new Person(
                    Guid.Parse("16c09865-f0ba-40cc-b845-e83686a2c7d3"),
                    new Domain.Logic.Persons.Create.Command
                    {
                        GivenName = "Person",
                        Surname = "Without",
                        GenderId = SelectValueMappingSeedData.PersonGenderMappings[0].Id,
                        AboutMe = "I'm a person without a user",
                        DateOfBirth = new DateTime(1981, 5, 7),
                        BirthName = "User",
                        Birthplace = "Wherethepfefferwächst",
                    });
                var contactDetail = new ContactDetail(Guid.Parse("c56fb43d-6500-4cc7-957c-d64baf049df2"), new Domain.Logic.ContactDetails.Create.Command
                {
                    PersonId = person.Id,
                    TypeId = SelectValueMappingSeedData.ContactDetailTypeMappings[0].Id,
                    Key = ContactDetailKey.EMail,
                    Value = "person@without.user"
                });
                person.ContactDetails.Add(contactDetail);
                return person;
            }
        }

        public static Person UnconfirmedUser
        {
            get
            {
                var person = new Person(
                    Guid.Parse("0bf0bd72-abda-458b-a783-403b8ba51850"),
                    new UserRegister.Command
                    {
                        GivenName = "Unconfirmed",
                        Surname = "User",
                        GenderId = SelectValueMappingSeedData.PersonGenderMappings[2].Id
                    });
                person.SetProperty(nameof(Person.ContactViaId), LockedOutUser.Id);
                person.BankAccounts.Add(new BankAccount(
                        Guid.Parse("1fa6a1f9-963c-4539-a3d3-e9e9b9430882"),
                        new Create.Command
                        {
                            PersonId = person.Id,
                            Iban = "DE95680900000037156400",
                            Bic = "GENODE61FR1",
                            CommentInner = "Dieses Konto läuft auf meine Mudda",
                            AccountOwner = "Muddi Roese"
                        }));
                return person;
            }
        }

        public static Person Person1WithSameEmail
        {
            get
            {
                var person = new Person(
                    Guid.Parse("e7c09d3e-8bd6-48b7-aedd-886b3ec1c323"),
                    new Domain.Logic.Persons.Create.Command
                    {
                        GivenName = "Person1",
                        Surname = "With",
                        GenderId = SelectValueMappingSeedData.PersonGenderMappings[1].Id,
                        DateOfBirth = new DateTime(1981, 6, 7),
                        BirthName = "Same Email",
                        Birthplace = "Cottbus",
                    });
                var contactDetail = new ContactDetail(Guid.Parse("2d81d902-6d5f-4d15-bc20-27e3d54d3484"), new Domain.Logic.ContactDetails.Create.Command
                {
                    PersonId = person.Id,
                    TypeId = SelectValueMappingSeedData.ContactDetailTypeMappings[0].Id,
                    Key = ContactDetailKey.EMail,
                    Value = "person@withsame.email"
                });
                person.ContactDetails.Add(contactDetail);
                return person;
            }
        }

        public static Person Person2WithSameEmail
        {
            get
            {
                var person = new Person(
                    Guid.Parse("93bd7667-ea91-435b-9d7a-23d71dee46b9"),
                    new Domain.Logic.Persons.Create.Command
                    {
                        GivenName = "Person2",
                        Surname = "With",
                        GenderId = SelectValueMappingSeedData.PersonGenderMappings[2].Id,
                        DateOfBirth = new DateTime(1981, 7, 7),
                        BirthName = "Same Email",
                        Birthplace = "Zwickau",
                    });
                var contactDetail = new ContactDetail(Guid.Parse("6638dcab-d415-4803-930f-ea13ead4e720"), new Domain.Logic.ContactDetails.Create.Command
                {
                    PersonId = person.Id,
                    TypeId = SelectValueMappingSeedData.ContactDetailTypeMappings[0].Id,
                    Key = ContactDetailKey.EMail,
                    Value = "person@withsame.email"
                });
                person.ContactDetails.Add(contactDetail);
                return person;
            }
        }

        public static Person PersonWithoutEmail
        {
            get
            {
                var person = new Person(
                    Guid.Parse("d4cf390f-c48f-4405-8979-2e2cfc227fed"),
                    new Domain.Logic.Persons.Create.Command
                    {
                        GivenName = "Person",
                        Surname = "Withoutemail",
                        GenderId = SelectValueMappingSeedData.PersonGenderMappings[2].Id,
                        DateOfBirth = new DateTime(1981, 7, 7),
                        Birthplace = "Kiew",
                    });
                return person;
            }
        }

        public static Person PersonWithMultipleEmails
        {
            get
            {
                var person = new Person(
                    Guid.Parse("6291f056-bd7e-4e2f-984a-85ef64f01b7c"),
                    new Domain.Logic.Persons.Create.Command
                    {
                        GivenName = "Person",
                        Surname = "Multiple",
                        GenderId = SelectValueMappingSeedData.PersonGenderMappings[2].Id,
                        DateOfBirth = new DateTime(1981, 7, 7),
                        BirthName = "Emails",
                        Birthplace = "Charkiw",
                    });
                var contactDetail1 = new ContactDetail(Guid.Parse("acc05ffa-877b-49a0-be2c-0e6b49b99252"), new Domain.Logic.ContactDetails.Create.Command
                {
                    PersonId = person.Id,
                    TypeId = SelectValueMappingSeedData.ContactDetailTypeMappings[0].Id,
                    Key = ContactDetailKey.EMail,
                    Value = "person@withmultiple.email"
                });
                person.ContactDetails.Add(contactDetail1);
                var contactDetail2 = new ContactDetail(Guid.Parse("40f18ff9-90cf-46be-9e06-6d288a27171d"), new Domain.Logic.ContactDetails.Create.Command
                {
                    PersonId = person.Id,
                    TypeId = SelectValueMappingSeedData.ContactDetailTypeMappings[0].Id,
                    Key = ContactDetailKey.EMail,
                    Value = "person@withmultiple2.email"
                });
                person.ContactDetails.Add(contactDetail2);
                return person;
            }
        }
    }
}
