using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Tests.Shared.TestSeedData
{
    public static class PersonSeedData
    {
        public static IList<Person> Persons
        {
            get
            {
                return new List<Person>
                {
                    Orsianer,
                    Orsonaut,
                    Orsoadmin,
                    UserWithoutRole,
                    DeletedUser
                };
            }
        }

        public static Person Orsianer
        {
            get
            {
                return new Person(
                    Guid.Parse("cb441176-eecb-4c56-908d-5a6afec36a95"),
                    new Domain.Auth.UserRegister.Command { GivenName = "Orsi", Surname = "Aner" });
            }
        }

        public static Person Orsonaut
        {
            get
            {
                return new Person(
                    Guid.Parse("c0c8470b-e6a0-4a0b-8a4c-24d503636248"),
                    new Domain.Auth.UserRegister.Command { GivenName = "Orso", Surname = "Naut" });
            }
        }

        public static Person Orsoadmin
        {
            get
            {
                return new Person(
                    Guid.Parse("8d960214-8f1b-4b69-8734-543aad67581c"),
                    new Domain.Auth.UserRegister.Command { GivenName = "Orso", Surname = "Admin" });
            }
        }

        public static Person UserWithoutRole
        {
            get
            {
                return new Person(
                    Guid.Parse("32e46032-125d-463a-87ed-67d9a34154c4"),
                    new Domain.Auth.UserRegister.Command { GivenName = "Without", Surname = "Role" });
            }
        }

        public static Person DeletedUser
        {
            get
            {
                return new Person(
                    Guid.Parse("4d98408b-620e-4ea5-9661-ab8efcad4495"),
                    new Domain.Auth.UserRegister.Command { GivenName = "Deleted", Surname = "User" });
            }
        }
    }
}
