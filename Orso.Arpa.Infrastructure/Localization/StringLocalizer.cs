using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace Orso.Arpa.Infrastructure.Localization
{
    public class StringLocalizer : IStringLocalizer
    {
        private readonly string _resourceKey;
        private readonly string _location;
        private readonly ILocalizerCache _cache;

        public StringLocalizer(string resourceKey, string location, ILocalizerCache cache)
        {
            _resourceKey = resourceKey;
            _location = location;
            _cache = cache;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            IList<LocalizedString> localizedStrings = new List<LocalizedString>();

            foreach (KeyValuePair<string, string> ls in _cache.GetAllTranslations(_resourceKey, _location))
            {
                localizedStrings.Add(new LocalizedString(ls.Key, ls.Value));
            }

            if (includeParentCultures)
            {
                foreach (KeyValuePair<string, string> ls in _cache.GetAllTranslations(_resourceKey, CultureInfo.GetCultureInfo(_location).Parent.ToString()))
                {
                    localizedStrings.Add(new LocalizedString(ls.Key, ls.Value));
                }
            }

            return localizedStrings;
        }

        public LocalizedString this[string name]
        {
            get
            {
                _cache.TryGetTranslation(name, _resourceKey, _location, out var translatedString);
                return new(name, translatedString);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => this[name];
    }
}
