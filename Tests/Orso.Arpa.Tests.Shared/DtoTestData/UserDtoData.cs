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
                    Performer,
                    Staff,
                    Admin,
                    UserWithoutRole,
                    LockedOutUser,
                    UnconfirmedUser
                };
            }
        }

        public static UserDto Performer
        {
            get
            {
                return new UserDto
                {
                    UserName = "performer",
                    RoleName = "Performer",
                    DisplayName = "Per Former",
                    RoleLevel = 1
                };
            }
        }

        public static UserDto Staff
        {
            get
            {
                return new UserDto
                {
                    UserName = "staff",
                    RoleName = "Staff",
                    DisplayName = "Staff Member",
                    RoleLevel = 2
                };
            }
        }

        public static UserDto Admin
        {
            get
            {
                return new UserDto
                {
                    UserName = "admin",
                    RoleName = "Admin",
                    DisplayName = "Ad Min",
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
