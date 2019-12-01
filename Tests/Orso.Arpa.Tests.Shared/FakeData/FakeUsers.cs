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
                    UserWithoutRole
                };
            }
        }

        public static User Orsianer
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("6F836C0E-E27D-4363-A67A-9DA92037A589"),
                    UserName = "orsianer",
                    Email = "orsianer@test.com",
                    PersonId = PersonSeedData.Orsianer.Id,
                    Person = PersonSeedData.Orsianer,
                    NormalizedEmail = "ORSIANER@TEST.COM",
                    NormalizedUserName = "ORSIANER"
                };
            }
        }

        public static User Orsonaut
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("DD0AA267-120E-4BD6-B6EB-02814C227E15"),
                    UserName = "orsonaut",
                    Email = "orsonaut@test.com",
                    PersonId = PersonSeedData.Orsonaut.Id,
                    Person = PersonSeedData.Orsonaut,
                    NormalizedEmail = "ORSONAUT@TEST.COM",
                    NormalizedUserName = "ORSONAUT"
                };
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
                    NormalizedUserName = "ORSOADMIN"
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
                    NormalizedUserName = "WITHOUTROLE"
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
                    NormalizedUserName = "DELETED"
                };
                user.Delete();
                return user;
            }
        }
    }
}
