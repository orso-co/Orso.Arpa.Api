using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Orso.Arpa.Api.Middleware
{
    public class ErrorResponseLocalizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizerFactory _localizerFactory;


        public ErrorResponseLocalizationMiddleware(RequestDelegate next,
            IStringLocalizerFactory localizerFactory)
        {
            _next = next;
            _localizerFactory = localizerFactory;
        }

        /// <summary>
        /// If the Response is an error message (StatusCode >= 400) then
        /// dependent on the culture information the <see cref="ValidationProblemDetails"/> is manipulated.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <returns>A <see cref="Task"/> that completes when the middleware has completed processing.</returns>
        public async Task Invoke(HttpContext context)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            var localizer = _localizerFactory.Create("Validator", culture);

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

                    try {
                        ValidationProblemDetails deserializedErrorMessage =
                            JsonSerializer.Deserialize<ValidationProblemDetails>(responseBody);

                        deserializedErrorMessage!.Detail =
                            deserializedErrorMessage.Detail.IsNullOrEmpty()
                                ? null: localizer[deserializedErrorMessage.Detail];

                        deserializedErrorMessage.Title = deserializedErrorMessage.Title.IsNullOrEmpty()
                            ? null : localizer[deserializedErrorMessage.Title];

                        await using var streamWrite = new StreamWriter(originalBody);

                        await streamWrite.WriteAsync(
                            JsonSerializer.Serialize(deserializedErrorMessage));
                    }
                    catch (Exception)
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
