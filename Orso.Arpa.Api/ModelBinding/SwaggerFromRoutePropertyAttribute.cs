using System;

namespace Orso.Arpa.Api.ModelBinding
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class SwaggerFromRoutePropertyAttribute : Attribute
    {
        public SwaggerFromRoutePropertyAttribute(string parameter)
        {
            Parameter = parameter;
        }

        public string Parameter { get; set; }
    }
}
