using System;
using System.Net;
using System.Net.Mime;
using System.Security.Authentication;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orso.Arpa.Domain.Errors;
using Orso.Arpa.Mail;

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
                case IdentityException ie:
                    _logger.LogError(ie, "IDENTITY ERROR");
                    errorMessage = new ErrorMessage { Message = ie.Message, Errors = ie.IdentityErrors };
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

                case ValidationException ve:
                    _logger.LogError(ve, "DOMAIN VALIDATION ERROR");
                    errorMessage = new ErrorMessage { Message = ve.Message, Errors = ve.Errors };
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case AuthenticationException ae:
                    _logger.LogError(ae, "AUTHENTICATION ERROR");
                    errorMessage = new ErrorMessage { Message = ae.Message };
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case NotFoundException nfe:
                    _logger.LogError(nfe, "NOT FOUND ERROR");
                    errorMessage = new ErrorMessage
                    {
                        Message = nfe.Message,
                        Errors = new { Property = nfe.PropertyName, Message = $"{nfe.TypeName} not found" }
                    };
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case EmailException ee:
                    _logger.LogError(ee, "EMAIL ERROR");
                    errorMessage = new ErrorMessage { Message = ee.Message, Errors = new { InnerException = ee.InnerException?.Message } };
                    context.Response.StatusCode = (int)HttpStatusCode.FailedDependency;
                    break;

                case Exception e:
                    _logger.LogError(e, "SERVER ERROR");
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
