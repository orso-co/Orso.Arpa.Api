using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Auth;
using Orso.Arpa.Application.Auth.Dtos;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Application.Tests.AuthTests.CommandHandlerTests
{
    public class RegisterHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _jwtGenerator = Substitute.For<IJwtGenerator>();
            _handler = new Register.Handler(_userManager, _jwtGenerator);
        }

        private UserManager<User> _userManager;
        private IJwtGenerator _jwtGenerator;
        private Register.Handler _handler;

        [Test]
        public async Task Should_Register_User()
        {
            // Arrange
            string expectedToken = "TestToken";
            var command = new Register.Command
            {
                Email = "ludmilla@test.com",
                Password = UserSeedData.ValidPassword,
                UserName = "ludmilla"
            };
            _jwtGenerator.CreateTokenAsync(Arg.Any<User>()).Returns(expectedToken);

            // Act
            TokenDto dto = await _handler.Handle(command, new CancellationToken());

            // Assert
            dto.Token.Should().BeEquivalentTo(expectedToken);
        }
    }
}
