using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

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
            context.Request.EnableRewind();
            await _next(context);
        }
    }
}
