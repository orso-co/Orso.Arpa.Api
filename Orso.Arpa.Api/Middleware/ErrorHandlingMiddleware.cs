using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            ValidationProblemDetails errorMessage = null;
            switch (ex)
            {
                case IdentityException ie:
                    errorMessage = new ValidationProblemDetails
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Title = ie.Message,
                        Detail = string.Join(". ", ie.IdentityErrors.Select(e => e.Description))
                    };
                    _logger.LogError(ie, "IDENTITY ERROR", errorMessage);
                    break;

                case ValidationException ve:
                    IEnumerable<string> errorCodes = ve.Errors.Select(e => e.ErrorCode);
                    if (errorCodes.Contains("404"))
                    {
                        errorMessage = new ValidationProblemDetails
                        {
                            Title = "Resource not found.",
                            Status = (int)HttpStatusCode.NotFound
                        };
                        _logger.LogError(ve, "NOT FOUND ERROR", errorMessage);
                    }
                    else if (errorCodes.Contains("403"))
                    {
                        errorMessage = new ValidationProblemDetails
                        {
                            Title = ve.Errors.First(e => e.ErrorCode.Equals("403")).ErrorMessage,
                            Status = (int)HttpStatusCode.Forbidden
                        };
                        _logger.LogError(ve, "AUTHORIZATION ERROR", errorMessage);
                    }
                    else if (errorCodes.Contains("401"))
                    {
                        errorMessage = new ValidationProblemDetails
                        {
                            Title = ve.Errors.First(e => e.ErrorCode.Equals("401")).ErrorMessage,
                            Status = (int)HttpStatusCode.Unauthorized
                        };
                        _logger.LogError(ve, "AUTHENTICATION ERROR", errorMessage);
                    }
                    else
                    {
                        errorMessage = new ValidationProblemDetails
                        {
                            Title = "One or more validation errors occurred.",
                            Status = (int)HttpStatusCode.UnprocessableEntity
                        };
                        _logger.LogError(ve, "DOMAIN VALIDATION ERROR", errorMessage);
                    }
                    foreach (IGrouping<string, FluentValidation.Results.ValidationFailure> errorGrouping in ve.Errors.GroupBy(e => e.PropertyName))
                    {
                        errorMessage.Errors.Add(errorGrouping.Key, errorGrouping.Select(g => g.ErrorMessage).ToArray());
                    }
                    break;

                case AuthenticationException ae:
                    errorMessage = new ValidationProblemDetails
                    {
                        Title = ae.Message,
                        Status = (int)HttpStatusCode.Unauthorized,
                    };
                    _logger.LogError(ae, "AUTHENTICATION ERROR", errorMessage);
                    break;

                case NotFoundException nfe:
                    errorMessage = new ValidationProblemDetails
                    {
                        Title = "Resource not found.",
                        Status = (int)HttpStatusCode.NotFound
                    };
                    errorMessage.Errors.Add(nfe.PropertyName, new string[] { nfe.Message });
                    _logger.LogError(nfe, "NOT FOUND ERROR", errorMessage);
                    break;

                case EmailException ee:
                    errorMessage = new ValidationProblemDetails
                    {
                        Status = (int)HttpStatusCode.FailedDependency,
                        Title = ee.Message,
                        Detail = ee.InnerException?.Message
                    };
                    _logger.LogError(ee, "EMAIL ERROR", errorMessage);
                    break;

                case AuthorizationException aze:
                    errorMessage = new ValidationProblemDetails
                    {
                        Title = aze.Message,
                        Status = (int)HttpStatusCode.Forbidden
                    };
                    _logger.LogError(aze, "AUTHORIZATION ERROR", errorMessage);
                    break;

                case Exception e:
                    errorMessage = new ValidationProblemDetails
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Title = "An unexpected error occured",
                        Detail = e.Message
                    };
                    _logger.LogError(e, "SERVER ERROR", errorMessage);
                    break;
            }

            context.Response.StatusCode = errorMessage?.Status ?? 500;

            if (errorMessage != null)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                var serializedErrorMessage = JsonSerializer.Serialize(errorMessage, serializeOptions);
                await context.Response.WriteAsync(serializedErrorMessage);
            }
        }
    }
}
