using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.General.Configuration;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Application.AuthApplication.Services
{
    public class CookieSignIn : ICookieSignIn
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly SignInManager<User> _signInManager;
        private readonly ArpaUserManager _userManager;

        public CookieSignIn(
            ArpaUserManager userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor,
            JwtConfiguration jwtConfiguration,
            IJwtGenerator jwtGenerator)
        {
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _jwtConfiguration = jwtConfiguration;
            _jwtGenerator = jwtGenerator;
            _userManager = userManager;
        }

        public async Task<Task> SignInUser(User user)
        {
            ClaimsIdentity claimsIdentity = await _jwtGenerator.GetClaimsIdentity(user);

            return _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }
        public Task SignOutUser()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task<Task> RefreshSignIn(User user)
        {
            var claimsIdentity = await _jwtGenerator.GetClaimsIdentity(user);

            return _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

        public async Task<bool> IsCookieSignInPossible(User user, string password)
        {
            var IsPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
            var IsUserLockedOut = await _userManager.IsLockedOutAsync(user);

            return IsPasswordCorrect && !IsUserLockedOut;
        }
    }
}