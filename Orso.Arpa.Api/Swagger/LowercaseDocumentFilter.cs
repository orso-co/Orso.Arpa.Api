using System.Linq;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Orso.Arpa.Api.Swagger
{
    public class LowerCaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.ToDictionary(
                entry => string.Join('/', entry.Key.Split('/').Select(x => x.ToLower())),
                entry => entry.Value);

            swaggerDoc.Paths = [];

            foreach ((string key, IOpenApiPathItem value) in paths)
            {
                foreach (IOpenApiParameter param in value.Operations.SelectMany(o => o.Value.Parameters))
                {
                    if (param is OpenApiParameter openApiParam)
                    {
                        openApiParam.Name = openApiParam.Name.ToLower();
                    }
                }

                swaggerDoc.Paths.Add(key, value);
            }
        }
    }
}
