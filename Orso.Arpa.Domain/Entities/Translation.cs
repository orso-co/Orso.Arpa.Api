using System.Text.Json.Serialization;

namespace Orso.Arpa.Domain.Entities
{
    public class Translation : BaseEntity
    {

        public Translation(string key, string text, string localizationCulture, string resourceKey)
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

        public string Key { get; private set; }

        public string Text { get; private set; }

        public string LocalizationCulture { get; private set; }

        public string ResourceKey { get; private set; }
    }

}
