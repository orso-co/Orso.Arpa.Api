using Orso.Arpa.Application.Logic.Persons;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Tests.Shared.DtoTestData
{
    public static class PersonDtoData
    {
        public static PersonDto Orsianer
        {
            get
            {
                Person person = PersonSeedData.Orsianer;
                return CreateDto(person);
            }
        }

        public static PersonDto Orsonaut
        {
            get
            {
                Person person = PersonSeedData.Orsonaut;
                return CreateDto(person);
            }
        }

        public static PersonDto Orsoadmin
        {
            get
            {
                Person person = PersonSeedData.Orsoadmin;
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
