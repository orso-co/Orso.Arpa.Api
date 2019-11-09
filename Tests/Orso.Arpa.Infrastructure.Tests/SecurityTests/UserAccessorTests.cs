using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.Errors;
using Orso.Arpa.Domain;
using Orso.Arpa.Infrastructure.Security;
using Orso.Arpa.Tests.Shared.Identity;
using Orso.Arpa.Tests.Shared.TestSeedData;

namespace Orso.Arpa.Infrastructure.Tests.SecurityTests
{
    [TestFixture]
    public class UserAccessorTests
    {
        private IHttpContextAccessor _httpContextAccessor;
        private UserManager<User> _userManager;
        private UserAccessor _userAccessor;

        [SetUp]
        public void Setup()
        {
            _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _userManager = new FakeUserManager();
            _userAccessor = new UserAccessor(_httpContextAccessor, _userManager);
        }

        [Test]
        public void Should_Get_Current_UserName()
        {
            // Arrange
            var expectedUserName = "dumdidum";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, expectedUserName)
            };
            _httpContextAccessor.HttpContext.User.Claims.Returns(claims);

            // Act
            var username = _userAccessor.GetCurrentUserName();

            // Assert
            username.Should().Be(expectedUserName);
        }

        [Test]
        public void Should_Throw_Rest_Exception_If_No_UserName_Claim_Can_Be_Found()
        {
            // Arrange
            _httpContextAccessor.HttpContext.User.Returns(default(ClaimsPrincipal));

            // Act
            Func<string> fct = () => _userAccessor.GetCurrentUserName();

            // Assert
            fct.Should().Throw<RestException>();
        }

        [Test]
        public async Task Should_Get_Current_User()
        {
            // Arrange
            User expectedUser = UserSeedData.Orsianer;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, expectedUser.UserName)
            };
            _httpContextAccessor.HttpContext.User.Claims.Returns(claims);

            // Act
            User user = await _userAccessor.GetCurrentUserAsync();

            // Assert
            user.Should().BeEquivalentTo(expectedUser, opt => opt.Excluding(u => u.ConcurrencyStamp));
        }
    }
}
