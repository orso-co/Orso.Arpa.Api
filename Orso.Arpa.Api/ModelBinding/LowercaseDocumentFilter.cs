using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Orso.Arpa.Api.ModelBinding
{
    public class LowerCaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.ToDictionary(
                entry => string.Join('/', entry.Key.Split('/').Select(x => x.ToLower())),
                entry => entry.Value);

            swaggerDoc.Paths = new OpenApiPaths();

            foreach ((string key, OpenApiPathItem value) in paths)
            {
                foreach (OpenApiParameter param in value.Operations.SelectMany(o => o.Value.Parameters))
                {
                    param.Name = param.Name.ToLower();
                }

                swaggerDoc.Paths.Add(key, value);
            }
        }
    }
}
