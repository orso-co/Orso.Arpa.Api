using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Logic.Users;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class UserDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            User user = FakeUsers.Orsianer;
            UserDto expectedDto = UserDtoData.Orsianer;

            // Act
            UserDto dto = _mapper.Map<UserDto>(user);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto, opt => opt
                .Excluding(dest => dest.RoleName)
                .Excluding(dest => dest.RoleLevel));
        }
    }
}
