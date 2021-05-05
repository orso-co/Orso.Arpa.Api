using System.Text.Json.Serialization;

namespace Orso.Arpa.Domain.Entities
{
    public class Translation : BaseEntity
    {
        public Translation(Guid? id, string key, string text, string localizationCulture, string resourceKey) : base(id)
        {
            Key = key;
            Text = text;
            LocalizationCulture = localizationCulture;
            ResourceKey = resourceKey;
        }

        public Translation() : base(null)
        {

        }

        public Translation(Guid id, string key, string text, string localizationCulture,
            string resourceKey, string createdBy, DateTime createdAt, string modifiedBy,
            DateTime? modifiedAt, bool deleted) : base(id)
        {
            Key = key;
            Text = text;
            LocalizationCulture = localizationCulture;
            ResourceKey = resourceKey;
            base.Create(createdBy, createdAt);
            if (modifiedAt != null) {
                if (deleted)
                    Delete(modifiedBy, (DateTime)modifiedAt);
                else
                {
                    base.Modify(modifiedBy, (DateTime)modifiedAt);
                }
            }
        }

        public string Key { get; set; }

        public string Text { get; set; }

        public string LocalizationCulture { get; set; }

        public string ResourceKey { get; set; }

    }

}
