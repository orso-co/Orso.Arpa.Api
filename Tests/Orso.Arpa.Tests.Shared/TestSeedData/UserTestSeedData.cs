using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class UserTestSeedData
    {
        public static IList<User> Users
        {
            get
            {
                return new List<User> {
                    Performer,
                    Staff,
                    UserWithoutRole,
                    LockedOutUser,
                    UnconfirmedUser
                };
            }
        }

        public static User Performer
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("6F836C0E-E27D-4363-A67A-9DA92037A589"),
                    UserName = "performer",
                    Email = "performer@test.com",
                    PersonId = PersonTestSeedData.Performer.Id,
                    EmailConfirmed = true,
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

        public static User Staff
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("DD0AA267-120E-4BD6-B6EB-02814C227E15"),
                    UserName = "staff",
                    Email = "staff@test.com",
                    PersonId = PersonTestSeedData.Staff.Id,
                    EmailConfirmed = true,
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

        public static User UserWithoutRole
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("9E96F67D-6972-4889-BB64-6BCEED23D095"),
                    UserName = "withoutrole",
                    Email = "withoutrole@test.com",
                    PersonId = PersonTestSeedData.UserWithoutRole.Id,
                    EmailConfirmed = true,
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

        public static User LockedOutUser
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("b1571de4-bdf4-4c98-8a63-4f72428e36af"),
                    UserName = "lockedout",
                    Email = "lockedout@test.com",
                    PersonId = PersonTestSeedData.LockedOutUser.Id,
                    EmailConfirmed = true,
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }

        public static User UnconfirmedUser
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("a53b7c43-3168-4f9b-a643-29a12f114aa6"),
                    UserName = "unconfirmed",
                    Email = "unconfirmed@test.com",
                    PersonId = PersonTestSeedData.UnconfirmedUser.Id,
                    CreatedAt = FakeDateTime.UtcNow
                };
            }
        }
    }
}