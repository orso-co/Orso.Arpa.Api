using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.RoleApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.DtoTestData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class RoleDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<RoleDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            Role role = RoleSeedData.Performer;
            RoleDto expectedDto = RoleDtoData.Performer;

            // Act
            RoleDto dto = _mapper.Map<RoleDto>(role);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
