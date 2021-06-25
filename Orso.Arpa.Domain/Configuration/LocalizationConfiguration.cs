using System.Collections.Generic;

namespace Orso.Arpa.Domain.Configuration
{
    public class LocalizationConfiguration
    {
        public string DefaultCulture { get; set; }

        public IList<string> SupportedUiCultures { get; set; }

        public bool FallbackToParentCulture { get; set; }
    }
}
