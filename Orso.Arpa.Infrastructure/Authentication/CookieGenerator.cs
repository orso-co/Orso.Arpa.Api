using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.General.Interfaces;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Orso.Arpa.Domain.UserDomain.Model;
using System.Threading;

namespace Orso.Arpa.Infrastructure.Authentication
{
    public class CookieGenerator : ICookieGenerator
    {
        public Task<string> CreateCookie(User user, string remoteIpAddress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateTokensAsync(User user, string remoteIpAddress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


}
