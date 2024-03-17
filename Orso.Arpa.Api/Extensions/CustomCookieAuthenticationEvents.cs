using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Orso.Arpa.Api.Extensions
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        public CustomCookieAuthenticationEvents()
        {
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            await base.ValidatePrincipal(context);
        }
    }
}