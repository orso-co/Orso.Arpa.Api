using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain;

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

        public async Task Invoke(HttpContext context, IConfiguration configuration)
        {
            var userJson =
                context.Request.Headers["user"].FirstOrDefault() ?? string.Empty;

            User user = JsonConvert.DeserializeObject<User>(userJson);

            var token = BuildAuthenticationToken(
                configuration,
                user);

            context.Request.Headers.Add("Authorization", "Bearer " + token);

            await _next(context);
        }

        private string BuildAuthenticationToken(
            IConfiguration configuration,
            User user)
        {
            return _jwtGenerator.CreateToken(user);
        }
    }
}
