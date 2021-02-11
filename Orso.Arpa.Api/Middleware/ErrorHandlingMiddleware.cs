using System;
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
    public partial class ErrorHandlingMiddleware
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

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception ex)
        {
            ErrorMessage errorMessage = null;
            switch (ex)
            {
                case IdentityException ie:
                    errorMessage = new ErrorMessage
                    {
                        status = (int)HttpStatusCode.InternalServerError,
                        title = ie.Message,
                        description = string.Join(". ", ie.IdentityErrors.Select(e => e.Description))
                    };
                    _logger.LogError(ie, "IDENTITY ERROR", errorMessage, errorMessage);
                    break;

                case ValidationException ve:
                    errorMessage = new ErrorMessage
                    {
                        title = "One or more validation errors occurred",
                        status = (int)HttpStatusCode.BadRequest
                    };
                    foreach (IGrouping<string, FluentValidation.Results.ValidationFailure> errorGrouping in ve.Errors.GroupBy(e => e.PropertyName))
                    {
                        errorMessage.errors.Add(errorGrouping.Key, errorGrouping.Select(g => g.ErrorMessage).ToArray());
                    }
                    _logger.LogError(ve, "DOMAIN VALIDATION ERROR", errorMessage);
                    break;

                case AuthenticationException ae:
                    errorMessage = new ErrorMessage
                    {
                        title = ae.Message,
                        status = (int)HttpStatusCode.Unauthorized
                    };
                    _logger.LogError(ae, "AUTHENTICATION ERROR", errorMessage);
                    break;

                case NotFoundException nfe:
                    errorMessage = new ErrorMessage
                    {
                        title = nfe.Message,
                        status = (int)HttpStatusCode.NotFound
                    };
                    errorMessage.errors.Add(nfe.PropertyName, new string[] { $"{nfe.TypeName} not found" });
                    _logger.LogError(nfe, "NOT FOUND ERROR", errorMessage);
                    break;

                case EmailException ee:
                    errorMessage = new ErrorMessage
                    {
                        status = (int)HttpStatusCode.FailedDependency,
                        title = ee.Message,
                        description = ee.InnerException?.Message
                    };
                    _logger.LogError(ee, "EMAIL ERROR", errorMessage);
                    break;

                case AuthorizationException aze:
                    errorMessage = new ErrorMessage
                    {
                        title = aze.Message,
                        status = (int)HttpStatusCode.Forbidden
                    };
                    _logger.LogError(aze, "AUTHORIZATION ERROR", errorMessage);
                    break;

                case Exception e:
                    errorMessage = new ErrorMessage
                    {
                        status = (int)HttpStatusCode.InternalServerError,
                        title = "An unexpected error occured",
                        description = e.Message
                    };
                    _logger.LogError(e, "SERVER ERROR", errorMessage);
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
