using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Configuration;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Identity;
using Orso.Arpa.Domain.Interfaces;
using Orso.Arpa.Infrastructure.Authentication;
using Orso.Arpa.Persistence.Seed;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Infrastructure.Tests.SecurityTests
{
    [TestFixture]
    public class JwtGeneratorTests
    {
        [SetUp]
        public void Setup()
        {
            _configuration = new JwtConfiguration
            {
                TokenKey = "qwertzuiopaasdfghjklxcvbnm",
                Audience = "audience",
                Issuer = "issuer",
                AccessTokenExpiryInMinutes = 60
            };
            _userManager = new FakeUserManager();
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _arpaContext = Substitute.For<IArpaContext>();
            _jwtGenerator = new JwtGenerator(
                _configuration,
                _userManager,
                _arpaContext,
                _httpContextAccessor,
                new FakeDateTimeProvider());
        }

        private JwtGenerator _jwtGenerator;
        private JwtConfiguration _configuration;
        private ArpaUserManager _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        private IArpaContext _arpaContext;

        [Test]
        public async Task Should_Generate_Jwt_Token()
        {
            // Arrange
            User user = FakeUsers.Performer;
            _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            var token = await _jwtGenerator.CreateTokensAsync(user, "127.0.0.1");

            // Assert
            JwtSecurityToken decryptedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            decryptedToken.Claims.First(c => c.Type == "nameid").Value.Should().Be(user.UserName);
            decryptedToken.Claims.First(c => c.Type == "name").Value.Should().Be(user.DisplayName);
            decryptedToken.Claims.First(c => c.Type == "sub").Value.Should().Be(user.Id.ToString());
            decryptedToken.Claims.First(c => c.Type == "issuer/person_id").Value.Should().Be(user.PersonId.ToString());
            decryptedToken.Claims.First(c => c.Type == "role").Value.Should().BeEquivalentTo(RoleSeedData.Performer.Name);
            decryptedToken.Issuer.Should().Be(_configuration.Issuer);
            decryptedToken.Audiences.First().Should().Be(_configuration.Audience);
        }
    }
}
