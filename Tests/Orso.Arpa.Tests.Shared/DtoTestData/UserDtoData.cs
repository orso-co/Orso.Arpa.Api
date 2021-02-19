using System;
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
                var user = new UserDto
                {
                    UserName = "performer",
                    RoleNames = new[] { "Performer" },
                    DisplayName = "Per Former",
                    Email = "performer@test.com",
                    EmailConfirmed = true,
                };
                user.StakeholderGroupIds.Add(Guid.Parse("c2cfb7a0-4981-4dda-b988-8ba74957f6a4"));
                return user;
            }
        }

        public static UserDto Staff
        {
            get
            {
                return new UserDto
                {
                    UserName = "staff",
                    RoleNames = new[] { "Staff" },
                    DisplayName = "Staff Member",
                    Email = "staff@test.com",
                    EmailConfirmed = true
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
                    RoleNames = new[] { "Admin" },
                    DisplayName = "Ad Min",
                    Email = "admin@test.com",
                    EmailConfirmed = true
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
                    RoleNames = Array.Empty<string>(),
                    DisplayName = "Without Role",
                    Email = "withoutrole@test.com",
                    EmailConfirmed = true
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
                    RoleNames = Array.Empty<string>(),
                    DisplayName = "LockedOut User",
                    Email = "lockedout@test.com",
                    EmailConfirmed = true
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
                    RoleNames = Array.Empty<string>(),
                    DisplayName = "Unconfirmed User",
                    Email = "unconfirmed@test.com",
                    EmailConfirmed = false
                };
            }
        }
    }
}
