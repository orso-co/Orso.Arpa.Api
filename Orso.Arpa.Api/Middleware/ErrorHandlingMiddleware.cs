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
using Orso.Arpa.Domain.General.Errors;
using Orso.Arpa.Mail;

namespace Orso.Arpa.Api.Middleware
{
    public partial class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        private readonly static JsonSerializerOptions s_serializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

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
            string errorLogMessage = null;

            switch (ex)
            {
                case IdentityException ie:
                    errorMessage = new ValidationProblemDetails
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Title = ie.Message,
                        Detail = string.Join(". ", ie.IdentityErrors.Select(e => e.Description))
                    };
                    errorLogMessage = "IDENTITY ERROR";
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
                        _logger.LogWarning(ve, "NOT FOUND ERROR");
                    }
                    else if (errorCodes.Contains("403"))
                    {
                        errorMessage = new ValidationProblemDetails
                        {
                            Title = ve.Errors.First(e => e.ErrorCode.Equals("403")).ErrorMessage,
                            Status = (int)HttpStatusCode.Forbidden
                        };
                        _logger.LogWarning(ve, "AUTHORIZATION ERROR");
                    }
                    else if (errorCodes.Contains("401"))
                    {
                        errorMessage = new ValidationProblemDetails
                        {
                            Title = ve.Errors.First(e => e.ErrorCode.Equals("401")).ErrorMessage,
                            Status = (int)HttpStatusCode.Unauthorized
                        };
                        _logger.LogWarning(ve, "AUTHENTICATION ERROR");
                    }
                    else
                    {
                        errorMessage = new ValidationProblemDetails
                        {
                            Title = "One or more validation errors occurred.",
                            Status = (int)HttpStatusCode.UnprocessableEntity
                        };
                        foreach (IGrouping<string, FluentValidation.Results.ValidationFailure> errorGrouping in ve.Errors.GroupBy(e => e.PropertyName))
                        {
                            errorMessage.Errors.Add(errorGrouping.Key, [.. errorGrouping.Select(g => g.ErrorMessage)]);
                        }
                        _logger.LogWarning(ve, "DOMAIN VALIDATION ERROR");
                    }
                    break;

                case AuthenticationException ae:
                    errorMessage = new ValidationProblemDetails
                    {
                        Title = ae.Message,
                        Status = (int)HttpStatusCode.Unauthorized,
                    };
                    _logger.LogWarning(ae, "AUTHENTICATION ERROR");
                    break;

                case NotFoundException nfe:
                    errorMessage = new ValidationProblemDetails
                    {
                        Title = "Resource not found.",
                        Status = (int)HttpStatusCode.NotFound
                    };
                    errorMessage.Errors.Add(nfe.PropertyName, [nfe.Message]);
                    _logger.LogWarning(nfe, "NOT FOUND ERROR");
                    break;

                case EmailException ee:
                    errorMessage = new ValidationProblemDetails
                    {
                        Status = (int)HttpStatusCode.FailedDependency,
                        Title = ee.Message,
                        Detail = ee.InnerException?.Message
                    };
                    errorLogMessage = "EMAIL ERROR";
                    break;

                case AuthorizationException aze:
                    errorMessage = new ValidationProblemDetails
                    {
                        Title = aze.Message,
                        Status = (int)HttpStatusCode.Forbidden
                    };
                    _logger.LogWarning(aze, "AUTHORIZATION ERROR");
                    break;

                case Azure.RequestFailedException arfe:
                    errorMessage = new ValidationProblemDetails
                    {
                        Title = arfe.ErrorCode,
                        Status = arfe.Status,
                        Detail = arfe.Message
                    };
                    _logger.LogWarning(arfe, "NOT FOUND ERROR");
                    break;

                case AffectedRowCountMismatchException arcme:
                    errorMessage = new ValidationProblemDetails
                    {
                        Status = (int)HttpStatusCode.UnprocessableEntity,
                        Title = "The requested action could not be performed correctly on the database. Fewer changes were executed on the database than expected. Possibly, the sent object was already up to date."
                    };
                    errorLogMessage = arcme.Message;
                    break;

                default:
                    errorMessage = new ValidationProblemDetails
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Title = "An unexpected error occured",
                        Detail = ex.Message
                    };
                    errorLogMessage = "SERVER ERROR";
                    break;
            }

            context.Response.StatusCode = errorMessage?.Status ?? 500;
            context.Items.Add(
                SensibleRequestDataShadower.ASPNET_REQUEST_POSTED_BODY_SHADOWED,
                await SensibleRequestDataShadower.ShadowSensibleDataForLoggingAsync(context.Request.Body)
            );
            context.Response.ContentType = MediaTypeNames.Application.Json;

            if (errorLogMessage != null)
            {
                _logger.LogError(ex, message: "{ErrorLogMessage}", errorLogMessage);
                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException, message: "{ErrorLogMessage} - INNER EXCEPTION", errorLogMessage);
                }
            }

            var serializedErrorMessage = JsonSerializer.Serialize(errorMessage, s_serializerOptions);
            await context.Response.WriteAsync(serializedErrorMessage);
        }
    }
}
