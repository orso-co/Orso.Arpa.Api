using System.Collections.Generic;
using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class ReducedPersonDtoData
    {
        public static IList<ReducedPersonDto> Persons
        {
            get
            {
                return new List<ReducedPersonDto>
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
        public static ReducedPersonDto Performer
        {
            get
            {
                Person person = PersonTestSeedData.Performer;
                return CreateDto(person);
            }
        }

        public static ReducedPersonDto Staff
        {
            get
            {
                Person person = PersonTestSeedData.Staff;
                return CreateDto(person);
            }
        }

        public static ReducedPersonDto Admin
        {
            get
            {
                Person person = PersonSeedData.Admin;
                return CreateDto(person);
            }
        }

        public static ReducedPersonDto WithoutRole
        {
            get
            {
                Person person = PersonTestSeedData.UserWithoutRole;
                return CreateDto(person);
            }
        }

        public static ReducedPersonDto UnconfirmedUser
        {
            get
            {
                Person person = PersonTestSeedData.UnconfirmedUser;
                return CreateDto(person);
            }
        }

        public static ReducedPersonDto DeletedUser
        {
            get
            {
                Person person = PersonTestSeedData.DeletedUser;
                return CreateDto(person);
            }
        }

        public static ReducedPersonDto LockedOutUser
        {
            get
            {
                Person person = PersonTestSeedData.LockedOutUser;
                return CreateDto(person);
            }
        }

        private static ReducedPersonDto CreateDto(Person person)
        {
            return new ReducedPersonDto
            {
                GivenName = person.GivenName,
                Id = person.Id,
                Surname = person.Surname,
                DisplayName = person.DisplayName
            };
        }
    }
}
