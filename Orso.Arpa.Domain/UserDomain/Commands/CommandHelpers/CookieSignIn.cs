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
        private readonly SignInManager<User> _signInManager;
        private readonly ArpaUserManager _userManager;

        public CookieSignIn(
            ArpaUserManager userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor,
            JwtConfiguration jwtConfiguration)
        {
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _jwtConfiguration = jwtConfiguration;
            _userManager = userManager;
        }

        public Task SignInUserWithClaims(User user, string token)
        {
            // var claims = new List<Claim>
            // {
            //     new Claim("nameid", user.UserName),
            //     new Claim("name", user.DisplayName),
            //     new Claim("sub", user.Id.ToString()),
            //     new Claim($"{_jwtConfiguration.Issuer}/person_id", user.PersonId.ToString())
            // };

            // var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // foreach (var role in await _userManager.GetRolesAsync(user))
            // {
            //     claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            // }

            // return _httpContextAccessor.HttpContext.SignInAsync(
            //     CookieAuthenticationDefaults.AuthenticationScheme,
            //     new ClaimsPrincipal(claimsIdentity));

            var claims = new List<Claim>
            {
             new Claim("nameid", user.UserName),
             new Claim("name", user.DisplayName),
             new Claim("sub", user.Id.ToString()),
             new Claim($"{_jwtConfiguration.Issuer}/person_id", user.PersonId.ToString())
            };

            return _signInManager.SignInWithClaimsAsync(user, false, claims);
        }
        public Task SignOutUser()
        {
            return _signInManager.SignOutAsync();
        }

        public async Task<Task> RefreshSignIn(User user, string token)
        {
            var claims = new List<Claim>
            {
                new Claim("nameid", user.UserName),
                new Claim("name", user.DisplayName),
                new Claim("sub", user.Id.ToString()),
                new Claim($"{_jwtConfiguration.Issuer}/person_id", user.PersonId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

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