using System;
using System.Collections.Generic;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Logic.Auth;

namespace Orso.Arpa.Persistence.Seed
{
    public static class PersonSeedData
    {
        public static IList<Person> Persons
        {
            get
            {
                return new List<Person>
                {
                    Admin,
                };
            }
        }

        public static Person Admin
        {
            get
            {
                return new Person(
                    Guid.Parse("8d960214-8f1b-4b69-8734-543aad67581c"),
                    new UserRegister.Command { GivenName = "Ad", Surname = "Min" });
            }
        }
    }
}
