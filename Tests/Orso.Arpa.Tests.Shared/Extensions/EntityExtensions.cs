using System.Reflection;

namespace Orso.Arpa.Tests.Shared.Extensions
{
    public static class EntityExtensions
    {
        public static void SetProperty(this object entity, string propertyName, object propertyValue)
        {
            PropertyInfo property = entity.GetType().GetProperty(propertyName);

            if (property.SetMethod == null)
            {
                property = property.DeclaringType.GetProperty(propertyName);
            }

            property.SetValue(entity, propertyValue);
        }
    }
}
