using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    internal class AuthenticatedTestRequestMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IJwtGenerator _jwtGenerator;

        public AuthenticatedTestRequestMiddleware(
            RequestDelegate next,
            IJwtGenerator jwtGenerator)
        {
            _next = next;
            _jwtGenerator = jwtGenerator;
        }

        public async Task Invoke(HttpContext context)
        {
            var userJson =
                context.Request.Headers["user"].FirstOrDefault() ?? string.Empty;

            User user = JsonConvert.DeserializeObject<User>(userJson);

            var token = await _jwtGenerator.CreateTokensAsync(user);

            context.Request.Headers.Add("Authorization", "Bearer " + token);
            context.Request.Headers.Remove("user");

            await _next(context);
        }
    }
}
