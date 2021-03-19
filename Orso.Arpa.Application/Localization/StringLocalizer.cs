using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Orso.Arpa.Application.Localization
{
    public class StringLocalizer : IStringLocalizer
    {
        private readonly string _resourceKey;
        private readonly string _location;
        private readonly LocalizerCache _cache;

        public StringLocalizer(string resourceKey, string location, LocalizerCache cache)
        {
            _resourceKey = resourceKey;
            _location = location;
            _cache = cache;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new System.NotImplementedException();
        }

        public LocalizedString this[string name]
        {
            get
            {
                return new (name, _cache.GetTranslation(name, _resourceKey, _location));
            }
        }

        public LocalizedString this[string name, params object[] arguments] => this[name];
    }
}
