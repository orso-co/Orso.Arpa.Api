using System;
using MediatR;

namespace Orso.Arpa.Domain.Errors
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string typeName, string propertyName, IBaseRequest request)
            : base($"Entity '{typeName}' was not found.")
        {
            TypeName = typeName;
            PropertyName = propertyName;
            Request = request;
        }

        public string TypeName { get; set; }
        public string PropertyName { get; set; }
        public IBaseRequest Request { get; set; }
    }
}
