using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Orso.Arpa.Api.Resources.Culture.Errors;

namespace Orso.Arpa.Api.Middleware
{
    public class ErrorResponseLocalizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<ApiResource> _localizer;


        public ErrorResponseLocalizationMiddleware(RequestDelegate next,
            IStringLocalizer<ApiResource> localizer)
        {
            _next = next;
            _localizer = localizer;
        }

        /// <summary>
        /// If the Response is an error message (StatusCode >= 400) than
        /// dependent on the culture information the <see cref="ErrorMessage"/> is manipulated.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <returns>A <see cref="Task"/> that completes when the middleware has completed processing.</returns>
        public async Task Invoke(HttpContext context)
        {

            if (_localizer == null)
            {
                await _next(context);
                return;
            }

            Stream originalBody = context.Response.Body;

            try
            {
                await using var memStream = new MemoryStream();
                context.Response.Body = memStream;

                await _next(context);

                if (context.Response.StatusCode >= 400) // Error occurred
                {
                    memStream.Position = 0;
                    string responseBody = await new StreamReader(memStream).ReadToEndAsync();

                    ErrorMessage serializedErrorMessage =
                        JsonConvert.DeserializeObject<ErrorMessage>(responseBody);

                    if (serializedErrorMessage != null)
                    {
                        serializedErrorMessage.description =
                            serializedErrorMessage.description != null
                                ? _localizer[serializedErrorMessage.description]
                                : null;

                        serializedErrorMessage.title = serializedErrorMessage.title != null
                            ? _localizer[serializedErrorMessage.title]
                            : null;

                        await using var streamWrite = new StreamWriter(originalBody);
                        await streamWrite.WriteAsync(
                            JsonConvert.SerializeObject(serializedErrorMessage));
                    }
                    else
                    {
                        memStream.Position = 0;
                        await memStream.CopyToAsync(originalBody);
                    }
                }
                else // Error did not occur
                {
                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }
            } finally {
                context.Response.Body = originalBody;
            }
        }
    }

    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds the <see cref="ErrorResponseLocalizationMiddleware"/> to replace error responses
        /// depending on the culture information.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/>.</param>
        /// <returns>The <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseErrorResponseLocalizationMiddleware(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<ErrorResponseLocalizationMiddleware>();
        }
    }
}
