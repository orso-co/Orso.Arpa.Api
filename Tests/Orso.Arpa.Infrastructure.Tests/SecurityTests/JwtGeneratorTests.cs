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
                TokenKey = "42 1e 0a a9 da a9 1c 11 25 33 45 e4 57 3b 98 59 4b 85 65 42 cb 9d 59 4d af 2d 3d 13 9f d6 a5 c9 f4 42 59 3c e2 8d 72 68 08 b6 d1 94 9f 9d fb 93 7a b8 19 71 f8 f1 bb 01 55 10 98 42 9b e0 bb d1",
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
            _ = _arpaContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

            // Act
            var token = await _jwtGenerator.CreateTokensAsync(user, "127.0.0.1");

            // Assert
            JwtSecurityToken decryptedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            _ = decryptedToken.Claims.First(c => c.Type == "nameid").Value.Should().Be(user.UserName);
            _ = decryptedToken.Claims.First(c => c.Type == "name").Value.Should().Be(user.DisplayName);
            _ = decryptedToken.Claims.First(c => c.Type == "sub").Value.Should().Be(user.Id.ToString());
            _ = decryptedToken.Claims.First(c => c.Type == "issuer/person_id").Value.Should().Be(user.PersonId.ToString());
            _ = decryptedToken.Claims.First(c => c.Type == "role").Value.Should().BeEquivalentTo(RoleSeedData.Performer.Name);
            _ = decryptedToken.Issuer.Should().Be(_configuration.Issuer);
            _ = decryptedToken.Audiences.First().Should().Be(_configuration.Audience);
        }
    }
}
