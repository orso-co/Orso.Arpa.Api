using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Infrastructure.Authentication;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Infrastructure.Tests.SecurityTests
{
    [TestFixture]
    public class UserAccessorTests
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IArpaContext _arpaContext;
        private ArpaUserManager _userManager;
        private UserAccessor _userAccessor;

        [SetUp]
        public void Setup()
        {
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _userManager = new FakeUserManager();
            _arpaContext = Substitute.For<IArpaContext>();
            _userAccessor = new UserAccessor(_httpContextAccessor, _userManager, _arpaContext);
        }

        [Test]
        public void Should_Get_Current_UserName()
        {
            // Arrange
            const string expectedUserName = "dumdidum";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, expectedUserName)
            };
            _ = _httpContextAccessor.HttpContext.User.Claims.Returns(claims);

            // Act
            var username = _userAccessor.UserName;

            // Assert
            _ = username.Should().Be(expectedUserName);
        }

        [Test]
        public void Should_Get_Current_DisplyName()
        {
            // Arrange
            const string expectedDisplayName = "dumdidum";
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, expectedDisplayName)
            };
            _ = _httpContextAccessor.HttpContext.User.Claims.Returns(claims);

            // Act
            var displayName = _userAccessor.DisplayName;

            // Assert
            _ = displayName.Should().Be(expectedDisplayName);
        }

        [Test]
        public void Should_Throw_Authentication_Exception_If_No_UserName_Claim_Can_Be_Found()
        {
            // Arrange
            _ = _httpContextAccessor.HttpContext.User.Returns(default(ClaimsPrincipal));

            // Act
            Func<string> fct = () => _userAccessor.UserName;

            // Assert
            _ = fct.Should().Throw<AuthenticationException>();
        }

        [Test]
        public async Task Should_Get_Current_User()
        {
            // Arrange
            User expectedUser = FakeUsers.Performer;
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, expectedUser.UserName)
            };
            _ = _httpContextAccessor.HttpContext.User.Claims.Returns(claims);

            // Act
            User user = await _userAccessor.GetCurrentUserAsync();

            // Assert
            _ = user.Should().BeEquivalentTo(expectedUser, opt => opt
                .Excluding(ctx => ctx.Path.EndsWith("ConcurrencyStamp"))
                .Excluding(u => u.RefreshTokens)
                .Excluding(u => u.Person));
            _ = user.RefreshTokens.Count.Should().Be(expectedUser.RefreshTokens.Count);
            _ = user.RefreshTokens.First().Should().BeEquivalentTo(expectedUser.RefreshTokens.First(), opt => opt.Excluding(t => t.Id));
        }

        [Test]
        public async Task Should_Get_Current_Person()
        {
            // Arrange
            Person expectedPerson = FakePersons.Performer;
            var claims = new List<Claim>
            {
                new Claim("/person_id", expectedPerson.Id.ToString())
            };
            _ = _httpContextAccessor.HttpContext.User.Claims.Returns(claims);
            _ = _arpaContext.FindAsync<Person>(Arg.Any<object[]>(), Arg.Any<CancellationToken>()).Returns(expectedPerson);

            // Act
            Person person = await _userAccessor.GetCurrentPersonAsync();

            // Assert
            _ = person.Should().Be(expectedPerson);
        }
    }
}
