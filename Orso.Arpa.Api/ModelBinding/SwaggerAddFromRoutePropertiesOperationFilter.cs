using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Orso.Arpa.Api.ModelBinding
{
    public class SwaggerAddFromRoutePropertiesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            IEnumerable<string> defaultValues = context.ApiDescription.CustomAttributes()
                .Where(x => x.GetType() == typeof(SwaggerFromRoutePropertyAttribute))
                .Select(x => ((SwaggerFromRoutePropertyAttribute)x).Parameter.ToLower());

            foreach (OpenApiParameter param in operation.Parameters.Where(x => defaultValues.Contains(x.Name.ToLower())))
            {
                param.Description = FormatDescription(param.Description);
            }
        }

        private static string FormatDescription(string description)
        {
            const string dvDescription = "Don't set this parameter in the dto as it is taken from route";

            if (string.IsNullOrEmpty(description))
            {
                return dvDescription;
            }
            else
            {
                return $"{description}<br />{dvDescription}";
            }
        }
    }
}
