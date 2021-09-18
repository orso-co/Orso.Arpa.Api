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
                    Performer,
                    Staff,
                    Admin,
                    WithoutRole,
                    UnconfirmedUser,
                    DeletedUser,
                    LockedOutUser
                };
            }
        }
        public static PersonDto Performer
        {
            get
            {
                Person person = PersonTestSeedData.Performer;
                return CreateDto(person, "anonymous", FakeDateTime.UtcNow);
            }
        }

        public static PersonDto Staff
        {
            get
            {
                Person person = PersonTestSeedData.Staff;
                return CreateDto(person, "anonymous", FakeDateTime.UtcNow);
            }
        }

        public static PersonDto Admin
        {
            get
            {
                Person person = PersonSeedData.Admin;
                return CreateDto(person, null, null);
            }
        }

        public static PersonDto WithoutRole
        {
            get
            {
                Person person = PersonTestSeedData.UserWithoutRole;
                return CreateDto(person, "anonymous", FakeDateTime.UtcNow);
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
                return dto;
            }
        }

        public static PersonDto DeletedUser
        {
            get
            {
                Person person = PersonTestSeedData.DeletedUser;
                return CreateDto(person, "anonymous", FakeDateTime.UtcNow);
            }
        }

        public static PersonDto LockedOutUser
        {
            get
            {
                Person person = PersonTestSeedData.LockedOutUser;
                PersonDto dto = CreateDto(person, "anonymous", FakeDateTime.UtcNow);
                dto.ContactsRecommended.Add(ReducedPersonDtoData.UnconfirmedUser);
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
