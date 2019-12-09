using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Orso.Arpa.Api.Middleware
{
    public class EnableRequestBodyRewindMiddleware
    {
        private readonly RequestDelegate _next;

        public EnableRequestBodyRewindMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableBuffering();
            await _next(context);
        }
    }
}
