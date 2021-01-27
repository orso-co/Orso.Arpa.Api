using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Orso.Arpa.Api.Tests.IntegrationTests.Shared
{
    internal class TestRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public TestRequestMiddleware(
            RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Connection.RemoteIpAddress = new IPAddress(16885952);
            await _next(context);
        }
    }
}
