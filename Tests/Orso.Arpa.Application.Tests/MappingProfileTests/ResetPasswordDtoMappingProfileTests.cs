using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication;
using Orso.Arpa.Domain.Logic.Auth;
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
        public void Should_Map()
        {
            // Arrange
            var dto = new ResetPasswordDto
            {
                Password = UserSeedData.ValidPassword,
                UserName = UserSeedData.Orsianer.UserName,
                Token = "Token"
            };

            // Act
            ResetPassword.Command command = _mapper.Map<ResetPassword.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
