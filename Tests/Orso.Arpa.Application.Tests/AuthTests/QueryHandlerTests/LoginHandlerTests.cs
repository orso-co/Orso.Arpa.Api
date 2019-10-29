using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Auth;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.SeedData;

namespace Orso.Arpa.Application.Tests.AuthTests.QueryHandlerTests
{
    public class LoginHandlerTests
    {
        [SetUp]
        public void Setup()
        {
            _signInManager = new FakeSignInManager();
            _jwtGenerator = Substitute.For<IJwtGenerator>();
            _handler = new Login.Handler(_signInManager, _jwtGenerator);
        }

        private SignInManager<User> _signInManager;
        private IJwtGenerator _jwtGenerator;
        private Login.Handler _handler;

        [Test]
        public async Task Should_Login_User()
        {
            // Arrange
            string expectedToken = "TestToken";
            User user = UserSeedData.Egon;
            var query = new Login.Query { Email = user.Email, Password = UserSeedData.ValidPassword };
            _jwtGenerator.CreateToken(Arg.Any<User>()).Returns(expectedToken);

            // Act
            TokenDto dto = await _handler.Handle(query, new CancellationToken());

            // Assert
            dto.Token.Should().BeEquivalentTo(expectedToken);
        }
    }
}
