using System;

namespace Orso.Arpa.Domain.General.Errors
{
    [Serializable]
    public class AffectedRowCountMismatchException : Exception
    {
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
