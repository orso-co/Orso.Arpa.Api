using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orso.Arpa.Application.Errors;

namespace Orso.Arpa.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private class ErrorMessage
        {
            public string Message { get; set; }
            public object Errors { get; set; }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception ex)
        {
            ErrorMessage errorMessage = null;
            switch (ex)
            {
                case RestException re:
                    _logger.LogError(ex, "REST ERROR");
                    errorMessage = new ErrorMessage { Message = re.Message, Errors = re.Errors };
                    context.Response.StatusCode = (int)re.Code;
                    break;
                case IdentityException ie:
                    _logger.LogError(ex, "IDENTITY ERROR");
                    errorMessage = new ErrorMessage { Message = ie.Message, Errors = ie.IdentityErrors };
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                case Exception e:
                    _logger.LogError(ex, "SERVER ERROR");
                    errorMessage = new ErrorMessage { Message = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message };
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = MediaTypeNames.Application.Json;
            if (errorMessage != null)
            {
                var result = JsonConvert.SerializeObject(new
                {
                    errorMessage
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}
