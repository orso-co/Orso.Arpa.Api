using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
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
            _jwtGenerator = new JwtGenerator(_configuration, _userManager, _roleManager);
        }

        private JwtGenerator _jwtGenerator;
        private JwtConfiguration _configuration;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;

        [Test]
        public async Task Should_Generate_Jwt_Token()
        {
            // Arrange
            User user = FakeUsers.Orsianer;

            // Act
            var token = await _jwtGenerator.CreateTokenAsync(user);

            // Assert
            JwtSecurityToken decryptedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value.Should().Be(user.UserName);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value.Should().Be(user.DisplayName);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value.Should().BeEquivalentTo(RoleSeedData.Orsianer.Name);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == "RoleLevel")?.Value.Should().BeEquivalentTo(RoleSeedData.Orsianer.Level.ToString());
            decryptedToken.Issuer.Should().Be(_configuration.Issuer);
            decryptedToken.Audiences.FirstOrDefault().Should().Be(_configuration.Audience);
        }
    }
}
