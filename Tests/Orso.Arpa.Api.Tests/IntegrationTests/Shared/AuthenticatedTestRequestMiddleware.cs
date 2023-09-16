using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Orso.Arpa.Domain.General.Interfaces;
using Orso.Arpa.Domain.UserDomain.Model;
using Orso.Arpa.Domain.UserDomain.Repositories;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    internal class AuthenticatedTestRequestMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IJwtGenerator _jwtGenerator;
        private readonly ArpaUserManager _arpaUserManager;

        public AuthenticatedTestRequestMiddleware(
            RequestDelegate next,
            IJwtGenerator jwtGenerator,
            ArpaUserManager arpaUserManager)
        {
            _next = next;
            _jwtGenerator = jwtGenerator;
            _arpaUserManager = arpaUserManager;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Connection.RemoteIpAddress = new IPAddress(16885952);
            var username =
                context.Request.Headers["username"].FirstOrDefault() ?? string.Empty;

            User loadedUser = await _arpaUserManager.FindByNameAsync(username);
            var token = await _jwtGenerator.CreateTokensAsync(loadedUser, context.Connection.RemoteIpAddress.ToString(), new CancellationToken());

            context.Request.Headers.Append("Authorization", "Bearer " + token);
            context.Request.Headers.Remove("username");

            await _next(context);
        }
    }
}
