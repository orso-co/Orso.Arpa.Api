using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Orso.Arpa.Api.Extensions
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CustomCookieAuthenticationEvents(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private readonly static JsonSerializerOptions s_serializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        }

        public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            _httpContextAccessor.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new ValidationProblemDetails()
            {
                Title = "Invalid cookie supplied",
                Detail = "This request requires a valid cookie to be provided",
                Status = 401
            }, s_serializerOptions));
            return Task.CompletedTask;
        }

        public override Task RedirectToLogout(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        }

        public override Task RedirectToReturnUrl(RedirectContext<CookieAuthenticationOptions> context)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        }

        public override Task SignedIn(CookieSignedInContext context)
        {
            //List<Claim> claims = context.Principal.Claims.ToList();
            return Task.CompletedTask;
            ;
        }
    }
}

