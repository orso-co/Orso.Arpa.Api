using System;
using System.Collections.Generic;
using Orso.Arpa.Domain;

namespace Orso.Arpa.Tests.Shared.SeedData
{
    public static class UserSeedData
    {
        public static IList<User> Users
        {
            get
            {
                return new List<User> {
                    Egon,
                    Erna,
                    Fritz
                };
            }
        }


        public static User Egon
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("6F836C0E-E27D-4363-A67A-9DA92037A589"),
                    UserName = "egon",
                    Email = "egon@test.com"
                };
            }
        }

        public static User Erna
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("DD0AA267-120E-4BD6-B6EB-02814C227E15"),
                    UserName = "erna",
                    Email = "erna@test.com"
                };
            }
        }

        public static User Fritz
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("29CFA973-20D6-4603-91BA-6F4C1F79A6FA"),
                    UserName = "fritz",
                    Email = "fritz@test.com"
                };
            }
        }

        public static User DeletedUser
        {
            get
            {
                return new User
                {
                    Id = Guid.Parse("ddfa6a35-ba75-46a3-9f32-6cab236ef0a3"),
                    UserName = "mechthilde",
                    Email = "mechthilde@test.com"
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
