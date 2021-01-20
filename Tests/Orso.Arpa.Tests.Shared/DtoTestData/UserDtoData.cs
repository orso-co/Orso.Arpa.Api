using System.Collections.Generic;
using Orso.Arpa.Application.UserApplication;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class UserDtoData
    {
        public static IList<UserDto> Users
        {
            get
            {
                return new List<UserDto>
                {
                    Orsianer,
                    Orsonaut,
                    Orsoadmin,
                    UserWithoutRole,
                    LockedOutUser,
                    UnconfirmedUser
                };
            }
        }

        public static UserDto Orsianer
        {
            get
            {
                return new UserDto
                {
                    UserName = "orsianer",
                    RoleName = "Orsianer",
                    DisplayName = "Orsi Aner",
                    RoleLevel = 1
                };
            }
        }

        public static UserDto Orsonaut
        {
            get
            {
                return new UserDto
                {
                    UserName = "orsonaut",
                    RoleName = "Orsonaut",
                    DisplayName = "Orso Naut",
                    RoleLevel = 2
                };
            }
        }

        public static UserDto Orsoadmin
        {
            get
            {
                return new UserDto
                {
                    UserName = "orsoadmin",
                    RoleName = "Orsoadmin",
                    DisplayName = "Orso Admin",
                    RoleLevel = 3
                };
            }
        }

        public static UserDto UserWithoutRole
        {
            get
            {
                return new UserDto
                {
                    UserName = "withoutrole",
                    RoleName = null,
                    DisplayName = "Without Role",
                    RoleLevel = 0
                };
            }
        }

        public static UserDto LockedOutUser
        {
            get
            {
                return new UserDto
                {
                    UserName = "lockedout",
                    RoleName = null,
                    DisplayName = "LockedOut User",
                    RoleLevel = 0
                };
            }
        }

        public static UserDto UnconfirmedUser
        {
            get
            {
                return new UserDto
                {
                    UserName = "unconfirmed",
                    RoleName = null,
                    DisplayName = "Unconfirmed User",
                    RoleLevel = 0
                };
            }
        }
    }
}
