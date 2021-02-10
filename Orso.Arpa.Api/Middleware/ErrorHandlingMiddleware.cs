using System;
using System.Collections.Generic;
using System.Linq;
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
            public string title { get; set; }
            public string description { get; set; }
            public int status { get; set; }
            public Dictionary<string, string[]> errors { get; set; } = new Dictionary<string, string[]>();
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
                    errorMessage = new ErrorMessage
                    {
                        status = (int)HttpStatusCode.InternalServerError,
                        title = ie.Message,
                        description = string.Join(". ", ie.IdentityErrors.Select(e => e.Description))
                    };
                    break;

                case ValidationException ve:
                    _logger.LogError(ve, "DOMAIN VALIDATION ERROR");
                    errorMessage = new ErrorMessage { title = ve.Message ?? "One or more validation errors occurred", status = (int)HttpStatusCode.BadRequest };
                    foreach (IGrouping<string, FluentValidation.Results.ValidationFailure> errorGrouping in ve.Errors.GroupBy(e => e.PropertyName))
                    {
                        errorMessage.errors.Add(errorGrouping.Key, errorGrouping.Select(g => g.ErrorMessage).ToArray());
                    }
                    break;

                case AuthenticationException ae:
                    _logger.LogError(ae, "AUTHENTICATION ERROR");
                    errorMessage = new ErrorMessage { title = ae.Message, status = (int)HttpStatusCode.Unauthorized };
                    break;

                case NotFoundException nfe:
                    _logger.LogError(nfe, "NOT FOUND ERROR");
                    errorMessage = new ErrorMessage
                    {
                        title = nfe.Message,
                        status = (int)HttpStatusCode.NotFound
                    };
                    errorMessage.errors.Add(nfe.PropertyName, new string[] { $"{nfe.TypeName} not found" });
                    break;

                case EmailException ee:
                    _logger.LogError(ee, "EMAIL ERROR");
                    errorMessage = new ErrorMessage
                    {
                        status = (int)HttpStatusCode.FailedDependency,
                        title = ee.Message,
                        description = ee.InnerException?.Message
                    };
                    break;

                case AuthorizationException aze:
                    _logger.LogError(aze, "AUTHORIZATION ERROR");
                    errorMessage = new ErrorMessage { title = aze.Message, status = (int)HttpStatusCode.Forbidden };
                    break;

                case Exception e:
                    _logger.LogError(e, "SERVER ERROR");
                    errorMessage = new ErrorMessage { status = (int)HttpStatusCode.InternalServerError, title = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message };
                    break;
            }

            context.Response.StatusCode = errorMessage?.status ?? 500;

            if (errorMessage != null)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                var serializedErrorMessage = JsonConvert.SerializeObject(errorMessage);
                await context.Response.WriteAsync(serializedErrorMessage);
            }
        }
    }
}
