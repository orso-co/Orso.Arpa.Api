using System.Collections.Generic;
using Orso.Arpa.Application.PersonApplication.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.UserDomain.Model;
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
                return
                [
                    Performer,
                    Staff,
                    Admin,
                    WithoutRole,
                    UnconfirmedUser,
                    DeletedUser,
                    LockedOutUser
                ];
            }
        }
        public static ReducedPersonDto Performer
        {
            get
            {
                Person person = PersonTestSeedData.Performer;
                return CreateDto(person, UserTestSeedData.Performer);
            }
        }

        public static ReducedPersonDto Staff
        {
            get
            {
                Person person = PersonTestSeedData.Staff;
                return CreateDto(person, UserTestSeedData.Staff);
            }
        }

        public static ReducedPersonDto Admin
        {
            get
            {
                Person person = PersonSeedData.Admin;
                return CreateDto(person, UserSeedData.Admin);
            }
        }

        public static ReducedPersonDto WithoutRole
        {
            get
            {
                Person person = PersonTestSeedData.UserWithoutRole;
                return CreateDto(person, UserTestSeedData.UserWithoutRole);
            }
        }

        public static ReducedPersonDto UnconfirmedUser
        {
            get
            {
                Person person = PersonTestSeedData.UnconfirmedUser;
                return CreateDto(person, UserTestSeedData.UnconfirmedUser);
            }
        }

        public static ReducedPersonDto DeletedUser
        {
            get
            {
                Person person = PersonTestSeedData.DeletedUser;
                return CreateDto(person, null);
            }
        }

        public static ReducedPersonDto LockedOutUser
        {
            get
            {
                Person person = PersonTestSeedData.LockedOutUser;
                return CreateDto(person, UserTestSeedData.LockedOutUser);
            }
        }

        private static ReducedPersonDto CreateDto(Person person, User user)
        {
            return new ReducedPersonDto
            {
                GivenName = person.GivenName,
                Id = person.Id,
                Surname = person.Surname,
                DisplayName = person.DisplayName,
                UserEmail = user?.Email ?? string.Empty
            };
        }
    }
}
