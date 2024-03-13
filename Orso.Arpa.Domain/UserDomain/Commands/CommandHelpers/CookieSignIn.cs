using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Application.AuthApplication.Services
{
    public class CookieSignIn : ICookieSignIn
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<User> _signInManager;
        private readonly ArpaUserManager _userManager;

        public CookieSignIn(
            ArpaUserManager userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public Task SignInUserWithClaims(User user, string token)
        {
            List<Claim> claims = new List<Claim>{
                new Claim("JwtToken", token)
            };

            return _signInManager.SignInWithClaimsAsync(user, false, claims);
        }
        public Task SignOutUser()
        {
            return _signInManager.SignOutAsync();
        }

        public Task RefreshSignIn(string token)
        {
            List<Claim> claims = new List<Claim>{
                new Claim("JwtToken", token)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

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