using System;
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

        [JsonConstructor]
        protected Translation()
        {

        }

        public string Key { get; set; }

        public string Text { get; set; }

        public string LocalizationCulture { get; set; }

        public string ResourceKey { get; set; }
    }

}
