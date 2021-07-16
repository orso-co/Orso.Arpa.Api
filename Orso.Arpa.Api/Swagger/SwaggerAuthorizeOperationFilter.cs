using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Orso.Arpa.Api.Swagger
{
    public class SwaggerAuthorizeOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            IEnumerable<AuthorizeAttribute> authAttributes = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>();

            HandlePolicyAttributes(operation, authAttributes);

            HandleRoleAttributes(operation, authAttributes);
        }

        private static void HandleRoleAttributes(OpenApiOperation operation, IEnumerable<AuthorizeAttribute> authAttributes)
        {
            IEnumerable<string> roleAttributes = authAttributes
                .Where(attr => attr.Roles != null)
                .Select(attr => attr.Roles)
                .Distinct();

            if (roleAttributes.Any())
            {
                operation.Responses.Add("403 a", new OpenApiResponse
                {
                    Description = $"If current user does not have the role of '{roleAttributes.First()}'",
                    Content = new Dictionary<string, OpenApiMediaType>()
                    {
                        { "application/json", new OpenApiMediaType() { Schema = new OpenApiSchema() { Type = "object", Properties = new Dictionary<string, OpenApiSchema>() {
                            { "title", new OpenApiSchema() { Type = "string" } },
                            { "description", new OpenApiSchema() {Type="string" } },
                            { "status", new OpenApiSchema() { Type = "integer" } }
                        } } } }
                    }
                });
                var oAuthScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                };
            }
        }

        private static void HandlePolicyAttributes(OpenApiOperation operation, IEnumerable<AuthorizeAttribute> authAttributes)
        {
            IEnumerable<string> policyAttributes = authAttributes
                .Where(attr => attr.Policy != null)
                .Select(attr => attr.Policy)
                .Distinct();

            if (policyAttributes.Any())
            {
                operation.Responses.Add("403 b", new OpenApiResponse
                {
                    Description = $"If current user does not meet policy '{policyAttributes.First()}'",
                    Content = new Dictionary<string, OpenApiMediaType>()
                    {
                        { "application/json", new OpenApiMediaType() { Schema = new OpenApiSchema() { Type = "object", Properties = new Dictionary<string, OpenApiSchema>() {
                            { "title", new OpenApiSchema() { Type = "string" } },
                            { "description", new OpenApiSchema() {Type="string" } },
                            { "status", new OpenApiSchema() { Type = "integer" } }
                        } } } }
                    }
                });
                var oAuthScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                };
            }
        }
    }
}
