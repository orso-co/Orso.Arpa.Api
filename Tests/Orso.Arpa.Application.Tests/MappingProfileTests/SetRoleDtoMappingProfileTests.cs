using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Domain.UserDomain.Commands;
using Orso.Arpa.Domain.UserDomain.Enums;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class SetRoleDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<SetRoleDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map_SetRole_Command()
        {
            // Arrange
            var dto = new SetRoleDto
            {
                Username = UserTestSeedData.Performer.UserName,
                RoleNames = new[] { RoleNames.Performer, RoleNames.Staff }
            };

            // Act
            SetRole.Command command = _mapper.Map<SetRole.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }

        [Test]
        public void Should_Map_SendActivationInfo_Command()
        {
            // Arrange
            var dto = new SetRoleDto
            {
                Username = UserTestSeedData.Performer.UserName,
                RoleNames = new[] { RoleNames.Staff }
            };

            // Act
            SendActivationInfo.Command command = _mapper.Map<SendActivationInfo.Command>(dto);

            // Assert
            command.Username.Should().BeEquivalentTo(dto.Username);
        }

        [Test]
        public void Should_Map_SendQrCode_Command()
        {
            // Arrange
            var dto = new SetRoleDto
            {
                Username = UserTestSeedData.Performer.UserName,
                RoleNames = new[] { RoleNames.Staff }
            };

            // Act
            SendMyQRCode.Command command = _mapper.Map<SendMyQRCode.Command>(dto);

            // Assert
            command.Username.Should().BeEquivalentTo(dto.Username);
        }
    }
}
