using System;
using System.Collections.Generic;
using Orso.Arpa.Application.BankAccountApplication;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Entities;
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
                PersonDto dto = CreateDto(person, null, null);
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
