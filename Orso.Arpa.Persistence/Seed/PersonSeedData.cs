using System;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Persistence.Seed
{
    public static class PersonSeedData
    {
        public static Person GetInitialAdmin(InitialAdminConfiguration initialAdminConfiguration)
        {
            if (initialAdminConfiguration is null)
            {
                return Admin;
            }

            return new Person(
                    AdminPersonId,
                    new RegisterUser.Command
                    {
                        GivenName = initialAdminConfiguration.GivenName,
                        Surname = initialAdminConfiguration.Surname,
                        GenderId = initialAdminConfiguration.GenderId
                    });
        }

        public static Guid AdminPersonId => Guid.Parse("56ed7c20-ba78-4a02-936e-5e840ef0748c");

        public static Person Admin
        {
            get
            {
                return new Person(
                    AdminPersonId,
                    new RegisterUser.Command
                    {
                        GivenName = "Initial",
                        Surname = "Admin",
                        GenderId = SelectValueMappingSeedData.PersonGenderMappings[2].Id
                    });
            }
        }
    }
}
