using AutoMapper;
using FluentAssertions;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Model;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Application.Tests.MappingProfileTests
{
    [TestFixture]
    public class ForgotPasswordDtoMappingProfileTests
    {
        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ForgotPasswordDtoMappingProfile>());

            _mapper = new Mapper(config);
        }

        private IMapper _mapper;

        [Test]
        public void Should_Map()
        {
            // Arrange
            var dto = new ForgotPasswordDto
            {
                UsernameOrEmail = "Username",
                ClientUri = "http://localhost:4200/auth/resetpassword?token="
            };

            // Act
            ForgotPassword.Command command = _mapper.Map<ForgotPassword.Command>(dto);

            // Assert
            command.Should().BeEquivalentTo(dto);
        }
    }
}
