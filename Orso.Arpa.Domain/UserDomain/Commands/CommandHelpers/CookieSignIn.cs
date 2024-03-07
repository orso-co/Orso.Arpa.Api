using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Application.AuthApplication.Interfaces;
using Orso.Arpa.Domain.UserDomain.Commands;

namespace Orso.Arpa.Application.AuthApplication.Services
{
    public class CookieSignInService : ICookieSignInService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public CookieSignInService(
                IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
    }
}