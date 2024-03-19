using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Api.Extensions
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ArpaUserManager _arpaUserManager;

        public CustomCookieAuthenticationEvents(IHttpContextAccessor httpContextAccessor, ArpaUserManager arpaUserManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _arpaUserManager = arpaUserManager;

        }

        public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.Response.StatusCode = 403;
            return Task.CompletedTask;
        }

        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.Response.Clear();
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        }

        public override Task RedirectToLogout(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        }

        public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.Response.StatusCode = 401;
            return Task.CompletedTask;
        }

        public override Task SignedIn(CookieSignedInContext context)
        {
            List<Claim> claims = context.Principal.Claims.ToList();
            //_arpaUserManager.FindByEmailAsync(claims.)
            return Task.CompletedTask;
            ;
        }
    }
}