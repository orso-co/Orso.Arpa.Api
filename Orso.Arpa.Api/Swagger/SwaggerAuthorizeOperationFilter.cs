using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
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
                    Description = $"If current user does not have the role of '{string.Join(", ", roleAttributes)}'",
                    Content = CreateContent()
                });
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
                    Description = $"If current user does not meet policy '{string.Join(", ", policyAttributes)}'",
                    Content = CreateContent()
                });
            }
        }

        private static Dictionary<string, OpenApiMediaType> CreateContent()
        {
            return new Dictionary<string, OpenApiMediaType>()
            {
                {
                    "application/json",
                    new OpenApiMediaType()
                    {
                        Schema = new OpenApiSchema()
                        {
                            Type = JsonSchemaType.Object,
                            Properties = new Dictionary<string, IOpenApiSchema>()
                            {
                                { "title", new OpenApiSchema() { Type = JsonSchemaType.String } },
                                { "description", new OpenApiSchema() { Type = JsonSchemaType.String } },
                                { "status", new OpenApiSchema() { Type = JsonSchemaType.Integer } }
                            }
                        }
                    }
                }
            };
        }
    }
}
