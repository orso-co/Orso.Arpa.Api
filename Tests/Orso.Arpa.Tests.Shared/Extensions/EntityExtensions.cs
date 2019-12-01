namespace Orso.Arpa.Tests.Shared.Extensions
{
    public static class EntityExtensions
    {
        public static void SetProperty(this object entity, string propertyName, object propertyValue)
        {
            entity.GetType().GetProperty(propertyName).SetValue(entity, propertyValue);
        }
    }
}
