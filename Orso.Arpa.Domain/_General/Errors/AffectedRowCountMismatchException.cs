using System;
using System.Runtime.Serialization;

namespace Orso.Arpa.Domain.General.Errors
{
    [Serializable]
    public class AffectedRowCountMismatchException : Exception
    {
        protected AffectedRowCountMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {}

        public AffectedRowCountMismatchException(string entityName) : base(CreateMessage(entityName)) {
        }

        public AffectedRowCountMismatchException()
        {
        }

        public AffectedRowCountMismatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private static string CreateMessage(string entityName) {
            return $"The affected row count does not match the expected row count for entity {entityName}"; 
        }
    }
}
