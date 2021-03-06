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
                var person = new Person(
                    Guid.Parse("56ed7c20-ba78-4a02-936e-5e840ef0748c"),
                    new UserRegister.Command { GivenName = "Initial", Surname = "Admin" });
                person.Create("anonymous", new DateTime(2021,1,1));
                return person;
            }
        }
    }
}
