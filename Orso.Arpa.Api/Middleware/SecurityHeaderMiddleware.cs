using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Orso.Arpa.Api.Middleware
{
    public class SecurityHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Server = "Redacted";
            context.Response.Headers.XPoweredBy = "Redacted";
            context.Response.Headers.SetCommaSeparatedValues(
                "Permission-Policy",
                "accelerometer=(self",
                "ambient-light-sensor=(self)",
                "autoplay=(self)",
                "battery=(self)",
                "camera=(self)",
                "cross-origin-isolated=(self)",
                "display-capture=(self)",
                "document-domain=(self)",
                "encrypted-media=(self)",
                "execution-while-not-rendered=(self)",
                "execution-while-out-of-viewport=(self)",
                "fullscreen=(self)",
                "geolocation=(self)",
                "gyroscope=(self)",
                "keyboard-map=(self)",
                "magnetometer=(self)",
                "microphone=(self)",
                "midi=(self)",
                "navigation-override=(self)",
                "payment=(self)",
                "picture-in-picture=(self)",
                "publickey-credentials-get=(self)",
                "screen-wake-lock=(self)",
                "sync-xhr=(self)",
                "usb=(self)",
                "web-share=(self)",
                "xr-spatial-tracking=(self)",
                "clipboard-read=(self)",
                "clipboard-write=(self)",
                "gamepad=(self)",
                "speaker-selection=(self)",
                "conversion-measurement=(self)",
                "focus-without-user-activation=(self)",
                "hid=(self)",
                "idle-detection=(self)",
                "interest-cohort=(self)",
                "serial=(self)",
                "sync-script=(self)",
                "trust-token-redemption=(self)",
                "window-placement=(self)",
                "vertical-scroll=(self)");

            if (_next != null)
            {
                await _next(context);
            }
        }
    }
}
