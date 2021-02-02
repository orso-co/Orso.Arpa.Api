using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class LoginDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<LoginDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new LoginDto
            {
                Password = UserSeedData.ValidPassword,
                UsernameOrEmail = UserSeedData.Performer.UserName
            };

            // Act
            Login.Command command = _mapper.Map<Login.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
