using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class PersonDtoData
    {
        public static PersonDto Performer
        {
            get
            {
                Person person = PersonTestSeedData.Performer;
                return CreateDto(person, "anonymous");
            }
        }

        public static PersonDto Staff
        {
            get
            {
                Person person = PersonTestSeedData.Staff;
                return CreateDto(person, "anonymous");
            }
        }

        public static PersonDto Admin
        {
            get
            {
                Person person = PersonSeedData.Admin;
                return CreateDto(person, "anonymous");
            }
        }

        private static PersonDto CreateDto(Person person, string createdBy)
        {
            return new PersonDto
            {
                CreatedBy = createdBy,
                GivenName = person.GivenName,
                Id = person.Id,
                ModifiedAt = null,
                ModifiedBy = person.ModifiedBy,
                Surname = person.Surname,
                CreatedAt = new System.DateTime(2021,1,1)
            };
        }
    }
}
