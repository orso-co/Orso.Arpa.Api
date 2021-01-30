using Orso.Arpa.Application.PersonApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class PersonDtoData
    {
        public static PersonDto Performer
        {
            get
            {
                Person person = PersonSeedData.Performer;
                return CreateDto(person);
            }
        }

        public static PersonDto Staff
        {
            get
            {
                Person person = PersonSeedData.Staff;
                return CreateDto(person);
            }
        }

        public static PersonDto Admin
        {
            get
            {
                Person person = PersonSeedData.Admin;
                return CreateDto(person);
            }
        }

        private static PersonDto CreateDto(Person person)
        {
            return new PersonDto
            {
                CreatedBy = "anonymous",
                GivenName = person.GivenName,
                Id = person.Id,
                ModifiedAt = null,
                ModifiedBy = person.ModifiedBy,
                Surname = person.Surname
            };
        }
    }
}
