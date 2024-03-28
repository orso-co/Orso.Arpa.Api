using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NUnit.Framework;
using Orso.Arpa.Application.AuthApplication.Services;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;
using Orso.Arpa.Tests.Shared.FakeData;
using Orso.Arpa.Tests.Shared.Identity;

namespace Orso.Arpa.Domain.Tests.AuthTests.CommandHelperTests
{
    public class CookieSignInTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            _userManager = new FakeUserManager();
            _signInManager = new FakeSignInManager();
            _httpContextAccessor = new HttpContextAccessor();
            _jwtGenerator = Substitute.For<IJwtGenerator>();
            _cookieSignIn = new CookieSignIn(_userManager, _signInManager, _httpContextAccessor, _jwtGenerator);
        }


        private ArpaUserManager _userManager;
        private IJwtGenerator _jwtGenerator;
        private SignInManager<User> _signInManager;
        private IHttpContextAccessor _httpContextAccessor;
        private CookieSignIn _cookieSignIn;

        [Test]
        public async Task Should_Signin_User()
        {
            // Arrange
            User user = FakeUsers.Performer;
            _jwtGenerator.GetClaimsIdentity(Arg.Any<User>()).Returns(new ClaimsIdentity());

            // Act
            await _cookieSignIn.SignInUser(user);

            // Assert
            await _httpContextAccessor.HttpContext.Received().SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Arg.Any<ClaimsPrincipal>());
            // await _cookieSignIn.Received().SignInUser(Arg.Is<User>(u => u.Email == user.Email));
            // await _jwtGenerator.Received().CreateRefreshTokenAsync(Arg.Is<User>(u => u.Email == user.Email), Arg.Any<string>(), Arg.Any<CancellationToken>());
        }

    }
}