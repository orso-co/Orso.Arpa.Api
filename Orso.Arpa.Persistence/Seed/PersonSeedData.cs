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
        public static Guid PerformerPersonId => Guid.Parse("a7e3c2b1-4d5f-4a8e-9c6b-2d1e3f4a5b6c");
        public static Guid StaffPersonId => Guid.Parse("b8f4d3c2-5e6a-4b9f-8d7c-3e2f1a4b5c6d");

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

        public static Person Performer
        {
            get
            {
                return new Person(
                    PerformerPersonId,
                    new RegisterUser.Command
                    {
                        GivenName = "Test",
                        Surname = "Performer",
                        GenderId = SelectValueMappingSeedData.PersonGenderMappings[2].Id
                    });
            }
        }

        public static Person Staff
        {
            get
            {
                return new Person(
                    StaffPersonId,
                    new RegisterUser.Command
                    {
                        GivenName = "Test",
                        Surname = "Staff",
                        GenderId = SelectValueMappingSeedData.PersonGenderMappings[2].Id
                    });
            }
        }
    }
}
