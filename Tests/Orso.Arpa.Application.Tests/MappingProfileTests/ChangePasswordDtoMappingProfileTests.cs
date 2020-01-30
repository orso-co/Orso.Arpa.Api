using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.Dtos;
using Orso.Arpa.Application.MappingProfiles;
using Orso.Arpa.Domain.Logic.Auth;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ChangePasswordDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ChangePasswordDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new ChangePasswordDto
            {
                CurrentPassword = UserSeedData.ValidPassword,
                NewPassword = UserSeedData.ValidPassword + "new"
            };

            // Act
            ChangePassword.Command command = _mapper.Map<ChangePassword.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
