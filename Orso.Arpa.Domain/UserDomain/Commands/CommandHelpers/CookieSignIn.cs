using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Orso.Arpa.Application.AuthApplication.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Application.AuthApplication.Services
{
    public class CookieSignInService : ICookieSignInService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<User> _signInManager;
        private readonly ArpaUserManager _userManager;

        public CookieSignInService(
                ArpaUserManager userManager,
                SignInManager<User> signInManager,
                IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public Task RefreshSignIn(string token)
        {
            List<Claim> claims = new List<Claim>{
                    new Claim("JwtToken", token)
                };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var signInTask = _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return signInTask;
        }

        public Task SignInUserWithClaims(User user, string token)
        {
            List<Claim> claims = new List<Claim>{
                new Claim("JwtToken", token)
            };

            Task signInTask = _signInManager.SignInWithClaimsAsync(user, false, claims);

            return signInTask;
        }

        public async Task<bool> IsCookieSignInPossible(User user, string password)
        {
            var IsPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
            var IsUserLockedOut = await _userManager.IsLockedOutAsync(user);

            return IsPasswordCorrect && !IsUserLockedOut;
        }
    }
}