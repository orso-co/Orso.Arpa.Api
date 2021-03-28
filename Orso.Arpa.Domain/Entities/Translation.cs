using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Orso.Arpa.Domain.Interfaces;


namespace Orso.Arpa.Domain.Entities
{
    public class Translation : BaseEntity
    {

        [JsonConstructor]
        private Translation()
        {

        }

        public string Key { get; set; }

        public string Text { get; set; }

        public string LocalizationCulture { get; set; }

        public string ResourceKey { get; set; }

    }

}
