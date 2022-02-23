using System;
using System.Collections.Generic;
using Orso.Arpa.Application.BankAccountApplication;
using Orso.Arpa.Application.ContactDetailApplication;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Application.SelectValueApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class PersonDtoData
    {
        public static IList<PersonDto> Persons
        {
            get
            {
                return new List<PersonDto>
                {
                    Admin,
                    Performer,
                    Staff,
                    WithoutRole,
                    DeletedUser,
                    LockedOutUser,
                    UnconfirmedUser,
                    Person1WithSameEmail,
                    Person2WithSameEmail,
                    PersonWithoutUser,
                };
            }
        }
        public static PersonDto Performer
        {
            get
            {
                Person person = PersonTestSeedData.Performer;
                PersonDto dto = CreateDto(person, "anonymous", FakeDateTime.UtcNow);
                dto.Gender = SelectValueDtoData.Diverse;
                dto.StakeholderGroups.Add(SectionDtoData.Choir);
                dto.AboutMe = "Oh yes, I'm the great pretender!";
                dto.BirthName = "Sil";
                dto.Birthplace = "Pusemuckel";
                dto.DateOfBirth = new DateTime(1979, 5, 5);
                dto.Reliability = 3;
                dto.ExperienceLevel = 5;
                dto.GeneralPreference = 2;
                dto.ContactVia = ReducedPersonDtoData.UnconfirmedUser;
                return dto;
            }
        }

        public static PersonDto PerformerForNonStaff
        {
            get
            {
                Person person = PersonTestSeedData.Performer;
                PersonDto dto = CreateDto(person, "anonymous", FakeDateTime.UtcNow);
                dto.Gender = SelectValueDtoData.Diverse;
                dto.StakeholderGroups.Add(SectionDtoData.Choir);
                dto.AboutMe = "Oh yes, I'm the great pretender!";
                dto.BirthName = "Sil";
                dto.Birthplace = "Pusemuckel";
                dto.DateOfBirth = new DateTime(1979, 5, 5);
                return dto;
            }
        }

        public static PersonDto Staff
        {
            get
            {
                Person person = PersonTestSeedData.Staff;
                PersonDto dto = CreateDto(person, "anonymous", FakeDateTime.UtcNow);
                dto.Gender = SelectValueDtoData.Diverse;
                return dto;
            }
        }

        public static PersonDto Admin
        {
            get
            {
                Person person = PersonSeedData.Admin;
                PersonDto dto = CreateDto(person, "anonymous", FakeDateTime.UtcNow);
                dto.Gender = SelectValueDtoData.Diverse;
                return dto;
            }
        }

        public static PersonDto WithoutRole
        {
            get
            {
                Person person = PersonTestSeedData.UserWithoutRole;
                PersonDto dto = CreateDto(person, "anonymous", FakeDateTime.UtcNow);
                dto.Gender = SelectValueDtoData.Diverse;
                dto.Addresses.Add(new Application.AddressApplication.AddressDto
                {
                    Address1 = "Viktualienmarkt 4",
                    Address2 = "3. Etage rechts",
                    City = "München",
                    CommentInner = "Zweitwohnsitz",
                    Country = "Deutschland",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("df196870-5045-4a1c-b7fe-40473889830d"),
                    State = "Bayern",
                    Type = new SelectValueDto
                    {
                        Description = "",
                        Id = Guid.Parse("63437ce4-b63b-4558-9f91-1474b896bf1c"),
                        Name = "Business"
                    },
                    UrbanDistrict = "Altstadt-Lehel",
                    Zip = "80331"
                });
                dto.ContactDetails.Add(new ContactDetailDto
                {
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("252335a7-58ef-431b-ad1e-cc0ca53ebeaf"),
                    Key = ContactDetailKey.EMail,
                    TypeId = Guid.Parse("f0bf8326-623e-4caa-bd92-bc05c721a6cf"),
                    Value = "user@without.role"
                });
                return dto;
            }
        }

        public static PersonDto UnconfirmedUser
        {
            get
            {
                Person person = PersonTestSeedData.UnconfirmedUser;
                PersonDto dto = CreateDto(person, "anonymous", FakeDateTime.UtcNow);
                dto.ContactVia = ReducedPersonDtoData.LockedOutUser;
                dto.BankAccounts.Add(new BankAccountDto
                {
                    Bic = "GENODE61FR1",
                    CommentInner = "Dieses Konto läuft auf meine Mudda",
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Iban = "DE95680900000037156400",
                    Id = Guid.Parse("1fa6a1f9-963c-4539-a3d3-e9e9b9430882"),
                    AccountOwner = "Muddi Roese"
                });
                dto.Gender = SelectValueDtoData.Diverse;
                dto.ContactsRecommended.Add(ReducedPersonDtoData.Performer);
                return dto;
            }
        }

        public static PersonDto DeletedUser
        {
            get
            {
                Person person = PersonTestSeedData.DeletedUser;
                PersonDto dto = CreateDto(person, "anonymous", FakeDateTime.UtcNow);
                dto.Gender = SelectValueDtoData.Diverse;
                return dto;
            }
        }

        public static PersonDto LockedOutUser
        {
            get
            {
                Person person = PersonTestSeedData.LockedOutUser;
                PersonDto dto = CreateDto(person, "anonymous", FakeDateTime.UtcNow);
                dto.ContactsRecommended.Add(ReducedPersonDtoData.UnconfirmedUser);
                dto.Gender = SelectValueDtoData.Diverse;
                return dto;
            }
        }

        public static PersonDto PersonWithoutUser
        {
            get
            {
                Person person = PersonTestSeedData.PersonWithoutUser;
                PersonDto dto = CreateDto(person, "anonymous", FakeDateTime.UtcNow);
                dto.AboutMe = "I'm a person without a user";
                dto.Gender = SelectValueDtoData.Female;
                dto.BirthName = "User";
                dto.DateOfBirth = new DateTime(1981, 5, 7);
                dto.Birthplace = "Wherethepfefferwächst";
                dto.ContactDetails.Add(new ContactDetailDto
                {
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("c56fb43d-6500-4cc7-957c-d64baf049df2"),
                    Key = ContactDetailKey.EMail,
                    TypeId = Guid.Parse("f0bf8326-623e-4caa-bd92-bc05c721a6cf"),
                    Value = "person@without.user"
                });
                return dto;
            }
        }

        public static PersonDto Person1WithSameEmail
        {
            get
            {
                Person person = PersonTestSeedData.Person1WithSameEmail;
                PersonDto dto = CreateDto(person, "anonymous", FakeDateTime.UtcNow);
                dto.Gender = SelectValueDtoData.Male;
                dto.BirthName = "Same Email";
                dto.DateOfBirth = new DateTime(1981, 6, 7);
                dto.Birthplace = "Cottbus";
                dto.ContactDetails.Add(new ContactDetailDto
                {
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("2d81d902-6d5f-4d15-bc20-27e3d54d3484"),
                    Key = ContactDetailKey.EMail,
                    TypeId = Guid.Parse("f0bf8326-623e-4caa-bd92-bc05c721a6cf"),
                    Value = "person@withsame.email"
                });
                return dto;
            }
        }

        public static PersonDto Person2WithSameEmail
        {
            get
            {
                Person person = PersonTestSeedData.Person2WithSameEmail;
                PersonDto dto = CreateDto(person, "anonymous", FakeDateTime.UtcNow);
                dto.Gender = SelectValueDtoData.Diverse;
                dto.BirthName = "Same Email";
                dto.DateOfBirth = new DateTime(1981, 7, 7);
                dto.Birthplace = "Zwickau";
                dto.ContactDetails.Add(new ContactDetailDto
                {
                    CreatedAt = FakeDateTime.UtcNow,
                    CreatedBy = "anonymous",
                    Id = Guid.Parse("6638dcab-d415-4803-930f-ea13ead4e720"),
                    Key = ContactDetailKey.EMail,
                    TypeId = Guid.Parse("f0bf8326-623e-4caa-bd92-bc05c721a6cf"),
                    Value = "person@withsame.email"
                });
                return dto;
            }
        }

        private static PersonDto CreateDto(Person person, string createdBy, DateTime? createdAt)
        {
            return new PersonDto
            {
                CreatedBy = createdBy,
                GivenName = person.GivenName,
                Id = person.Id,
                ModifiedAt = null,
                ModifiedBy = person.ModifiedBy,
                Surname = person.Surname,
                CreatedAt = createdAt,
            };
        }
    }
}
