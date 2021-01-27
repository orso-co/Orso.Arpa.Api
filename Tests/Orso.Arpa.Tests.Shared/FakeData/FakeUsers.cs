using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.FakeData
{
    public static class FakeUsers
    {
        public static IList<User> Users
        {
            get
            {
                return new List<User> {
                    Orsianer,
                    Orsonaut,
                    Orsoadmin,
                    DeletedUser,
                    UserWithoutRole,
                    LockedOutUser,
                    UnconfirmedUser
                };
            }
        }

        public static User Orsianer
        {
            get
            {
                var user = new User
                {
                    Id = Guid.Parse("6F836C0E-E27D-4363-A67A-9DA92037A589"),
                    UserName = "orsianer",
                    Email = "orsianer@test.com",
                    PersonId = PersonSeedData.Orsianer.Id,
                    Person = FakePersons.Orsianer,
                    NormalizedEmail = "ORSIANER@TEST.COM",
                    NormalizedUserName = "ORSIANER",
                    EmailConfirmed = true
                };
                user.RefreshTokens.Add(new RefreshToken("orsianer_valid_refresh_token", DateTime.Now.AddDays(5), "127.0.0.1", user.Id));
                return user;
            }
        }

        public static User Orsonaut
        {
            get
            {
                var user = new User
                {
                    Id = Guid.Parse("DD0AA267-120E-4BD6-B6EB-02814C227E15"),
                    UserName = "orsonaut",
                    Email = "orsonaut@test.com",
                    PersonId = PersonSeedData.Orsonaut.Id,
                    Person = PersonSeedData.Orsonaut,
                    NormalizedEmail = "ORSONAUT@TEST.COM",
                    NormalizedUserName = "ORSONAUT",
                    EmailConfirmed = true
                };
                user.RefreshTokens.Add(new RefreshToken("orsonaut_expired_refresh_token", DateTime.Now.AddDays(-5), "127.0.0.1", user.Id));
                return user;
            }
        }

        public static User Orsoadmin
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("29CFA973-20D6-4603-91BA-6F4C1F79A6FA"),
                    UserName = "orsoadmin",
                    Email = "orsoadmin@test.com",
                    PersonId = PersonSeedData.Orsoadmin.Id,
                    Person = PersonSeedData.Orsoadmin,
                    NormalizedEmail = "ORSOADMIN@TEST.COM",
                    NormalizedUserName = "ORSOADMIN",
                    EmailConfirmed = true
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
                    PersonId = PersonSeedData.UserWithoutRole.Id,
                    Person = PersonSeedData.UserWithoutRole,
                    NormalizedEmail = "WOTHOUTROLE@TEST.COM",
                    NormalizedUserName = "WITHOUTROLE",
                    EmailConfirmed = true
                };
            }
        }

        public static User DeletedUser
        {
            get
            {
                var user = new User
                {
                    Id = Guid.Parse("ddfa6a35-ba75-46a3-9f32-6cab236ef0a3"),
                    UserName = "deleted",
                    Email = "deleted@test.com",
                    PersonId = PersonSeedData.DeletedUser.Id,
                    Person = PersonSeedData.DeletedUser,
                    NormalizedEmail = "DELETED@TEST.COM",
                    NormalizedUserName = "DELETED",
                    EmailConfirmed = true
                };
                user.Delete();
                return user;
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
                    PersonId = PersonSeedData.LockedOutUser.Id,
                    Person = PersonSeedData.LockedOutUser,
                    NormalizedEmail = "LOCKEDOUT@TEST.COM",
                    NormalizedUserName = "LOCKEDOUT",
                    EmailConfirmed = true
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
                    PersonId = PersonSeedData.UnconfirmedUser.Id,
                    Person = PersonSeedData.UnconfirmedUser,
                    NormalizedEmail = "UNCONFIRMED@TEST.COM",
                    NormalizedUserName = "UNCONFIRMED",
                    EmailConfirmed = false
                };
            }
        }
    }
}
