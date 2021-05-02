using System;
using System.Collections.Generic;
using Orso.Arpa.Application.UserApplication;
using Orso.Arpa.Tests.Shared.FakeData;

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
                    Id = Guid.Parse("6F836C0E-E27D-4363-A67A-9DA92037A589"),
                    UserName = "performer",
                    RoleNames = new[] { "Performer" },
                    DisplayName = "Per Former",
                    Email = "performer@test.com",
                    EmailConfirmed = true,
                    CreatedAt = FakeDateTime.UtcNow
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
                    Id = Guid.Parse("DD0AA267-120E-4BD6-B6EB-02814C227E15"),
                    UserName = "staff",
                    RoleNames = new[] { "Staff" },
                    DisplayName = "Staff Member",
                    Email = "staff@test.com",
                    EmailConfirmed = true,
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

        public static UserDto Admin
        {
            get
            {
                return new UserDto
                {
                    Id = Guid.Parse("b9ba1467-ad6f-40e5-a0c6-f482393b7feb"),
                    UserName = "admin",
                    RoleNames = new[] { "Admin" },
                    DisplayName = "Initial Admin",
                    Email = "admin@test.com",
                    EmailConfirmed = true,
                    CreatedAt = null
                };
            }
        }

        public static UserDto UserWithoutRole
        {
            get
            {
                return new UserDto
                {
                    Id = Guid.Parse("9E96F67D-6972-4889-BB64-6BCEED23D095"),
                    UserName = "withoutrole",
                    RoleNames = Array.Empty<string>(),
                    DisplayName = "Without Role",
                    Email = "withoutrole@test.com",
                    EmailConfirmed = true,
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

        public static UserDto LockedOutUser
        {
            get
            {
                return new UserDto
                {
                    Id = Guid.Parse("b1571de4-bdf4-4c98-8a63-4f72428e36af"),
                    UserName = "lockedout",
                    RoleNames = Array.Empty<string>(),
                    DisplayName = "LockedOut User",
                    Email = "lockedout@test.com",
                    EmailConfirmed = true,
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

        public static UserDto UnconfirmedUser
        {
            get
            {
                return new UserDto
                {
                    Id = Guid.Parse("a53b7c43-3168-4f9b-a643-29a12f114aa6"),
                    UserName = "unconfirmed",
                    RoleNames = Array.Empty<string>(),
                    DisplayName = "Unconfirmed User",
                    Email = "unconfirmed@test.com",
                    EmailConfirmed = false,
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }
    }
}
