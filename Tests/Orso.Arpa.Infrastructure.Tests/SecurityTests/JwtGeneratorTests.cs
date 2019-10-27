using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Infrastructure.Security;
using Orso.Arpa.Tests.Shared.SeedData;

namespace Orso.Arpa.Infrastructure.Tests.SecurityTests
{
    [TestFixture]
    public class JwtGeneratorTests
    {
        [SetUp]
        public void Setup()
        {
            _configuration = Substitute.For<IConfiguration>();
            _jwtGenerator = new JwtGenerator(_configuration);

        }

        private JwtGenerator _jwtGenerator;
        private IConfiguration _configuration;

        [Test]
        public void Should_Generate_Jwt_Token()
        {
            // Arrange
            _configuration["TokenKey"].Returns("qwertzuiopaasdfghjklxcvbnm");
            Domain.User user = UserSeedData.Egon;

            // Act
            var token = _jwtGenerator.CreateToken(user);

            // Assert
            JwtSecurityToken decryptedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            decryptedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value.Should().Be(user.UserName);
        }
    }
}
