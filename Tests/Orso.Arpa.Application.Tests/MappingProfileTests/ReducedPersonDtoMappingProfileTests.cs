using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.PersonApplication.Model;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ReducedPersonDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ReducedPersonDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private Mapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Person person = PersonTestSeedData.Performer;

            var expectedDto = new ReducedPersonDto
            {
                GivenName = person.GivenName,
                Id = person.Id,
                Surname = person.Surname,
                DisplayName = person.DisplayName
            };

            // Act
            ReducedPersonDto dto = _mapper.Map<ReducedPersonDto>(person);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
