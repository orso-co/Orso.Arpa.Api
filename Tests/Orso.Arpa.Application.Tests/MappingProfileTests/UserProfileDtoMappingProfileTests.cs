using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.MeApplication;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.DtoTestData;
using Orso.Arpa.Tests.Shared.FakeData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class UserProfileDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MyProfileDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            User user = FakeUsers.Performer;
            MyUserProfileDto expectedDto = UserProfileDtoData.Performer;

            // Act
            MyUserProfileDto dto = _mapper.Map<MyUserProfileDto>(user);

            // Assert
            dto.Should().BeEquivalentTo(expectedDto);
        }
    }
}
