using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Seed;
using Orso.Arpa.Infrastructure.Security;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Infrastructure.Tests.SecurityTests
{
    [TestFixture]
    public class JwtGeneratorTests
    {
        [SetUp]
        public void Setup()
        {
            _configuration = Substitute.For<IConfiguration>();
            _userManager = new FakeUserManager();
            _jwtGenerator = new JwtGenerator(_configuration, _userManager);

        }

        private JwtGenerator _jwtGenerator;
        private IConfiguration _configuration;
        private UserManager<User> _userManager;

        [Test]
        public async Task Should_Generate_Jwt_Token()
        {
            // Arrange
            _configuration["TokenKey"].Returns("qwertzuiopaasdfghjklxcvbnm");
            User user = UserSeedData.Orsianer;

            // Act
            var token = await _jwtGenerator.CreateTokenAsync(user);

            // Assert
            JwtSecurityToken decryptedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value.Should().Be(user.UserName);
            decryptedToken.Claims.Where(c => c.Type == "role").Select(c => c.Value).Should().BeEquivalentTo(RoleSeedData.Orsianer.Name);
        }
    }
}
