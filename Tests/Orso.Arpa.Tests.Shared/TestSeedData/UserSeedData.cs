using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class UserSeedData
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
                    LockedOutUser
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
                    PersonId = PersonSeedData.Orsianer.Id
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
                    PersonId = PersonSeedData.Orsonaut.Id
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
                    PersonId = PersonSeedData.Orsoadmin.Id
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
                    PersonId = PersonSeedData.UserWithoutRole.Id
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
                    PersonId = PersonSeedData.DeletedUser.Id
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
                    PersonId = PersonSeedData.LockedOutUser.Id
                };
            }
        }

        public static string ValidPassword
        {
            get
            {
                return "Pa$$w0rd";
            }
        }
    }
}
