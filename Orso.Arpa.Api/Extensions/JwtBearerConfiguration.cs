using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Orso.Arpa.Domain.General.Configuration;

namespace Orso.Arpa.Api.Extensions
{
    public static class JwtBearerConfiguration
    {
        private static readonly JsonSerializerOptions s_serializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

    public static AuthenticationBuilder AddJwtBearerConfiguration(this AuthenticationBuilder builder, JwtConfiguration jwtConfiguration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.TokenKey));

            return builder.AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.MapInboundClaims = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = new TimeSpan(0, 0, 30),
                    RequireExpirationTime = true,
                    RequireSignedTokens = true,

                    IssuerSigningKey = key,
                    ValidAudience = jwtConfiguration.Audience,
                    ValidIssuer = jwtConfiguration.Issuer,

                    // With MapInboundClaims = false, we need to explicitly set claim types
                    NameClaimType = "sub",
                    RoleClaimType = "role",
                };
                opt.Events = new JwtBearerEvents
                {
                    // SignalR sends JWT token as query parameter because WebSockets can't use Authorization header
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        // Ensure we always have an error and error description.
                        if (string.IsNullOrEmpty(context.Error))
                        {
                            context.Error = "Invalid token supplied";
                        }

                        if (string.IsNullOrEmpty(context.ErrorDescription))
                        {
                            context.ErrorDescription = "This request requires a valid JWT access token to be provided";
                        }

                        // Add some extra context for expired tokens.
                        if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            var authenticationException = context.AuthenticateFailure as SecurityTokenExpiredException;
                            context.Response.Headers.Append("x-token-expired", authenticationException.Expires.ToString("o"));
                            context.ErrorDescription = $"The token expired on {authenticationException.Expires:o}";
                        }

                        return context.Response.WriteAsync(JsonSerializer.Serialize(new ValidationProblemDetails()
                        {
                            Title = context.Error,
                            Detail = context.ErrorDescription,
                            Status = 401
                        }, s_serializerOptions));
                    }
                };
            });
        }
    }
}
