using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ResetPasswordDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ResetPasswordDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map_ResetPasswordCommand()
        {
            // Arrange
            var dto = new ResetPasswordDto
            {
                Password = UserSeedData.ValidPassword,
                UsernameOrEmail = UserTestSeedData.Performer.UserName,
                Token = "token%2B"
            };
            var expectedCommand = new ResetPassword.Command
            {
                Password = dto.Password,
                UsernameOrEmail = dto.UsernameOrEmail,
                Token = "token+"
            };

            // Act
            ResetPassword.Command command = _mapper.Map<ResetPassword.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }

        [Test]
        public void Should_Map_SendChangedPasswordInfoCommand()
        {
            // Arrange
            var dto = new ResetPasswordDto
            {
                Password = UserSeedData.ValidPassword,
                UsernameOrEmail = UserTestSeedData.Performer.UserName,
                Token = "token%2B"
            };
            var expectedCommand = new SendPasswordChangedInfo.Command
            {
                UsernameOrEmail = dto.UsernameOrEmail,
            };

            // Act
            SendPasswordChangedInfo.Command command = _mapper.Map<SendPasswordChangedInfo.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(expectedCommand);
        }
    }
}
