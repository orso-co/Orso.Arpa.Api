using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
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
            _roleManager = new FakeRoleManager();
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _arpaContext = Substitute.For<IArpaContext>();
            _jwtGenerator = new JwtGenerator(_configuration, _userManager, _roleManager, _arpaContext, _httpContextAccessor);
        }

        private JwtGenerator _jwtGenerator;
        private JwtConfiguration _configuration;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
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
            decryptedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value.Should().Be(user.UserName);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value.Should().Be(user.DisplayName);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value.Should().BeEquivalentTo(RoleSeedData.Performer.Name);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == "RoleLevel")?.Value.Should().BeEquivalentTo(RoleSeedData.Performer.Level.ToString());
            decryptedToken.Issuer.Should().Be(_configuration.Issuer);
            decryptedToken.Audiences.FirstOrDefault().Should().Be(_configuration.Audience);
        }
    }
}
