using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper.Internal;
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
            IList<LocalizedString> localizedStrings = new List<LocalizedString>();

            CultureInfo culture = CultureInfo.GetCultures(CultureTypes.AllCultures).AsQueryable()
                .First(q => q.Name.Equals(_location));
            _cache.GetAllTranslations(culture.ToString(), _resourceKey).ForAll(ls =>
                localizedStrings.Add(new LocalizedString(ls.Key, ls.Text)));

            if (includeParentCultures)
            {
                var parentCulture = culture.Parent;
                _cache.GetAllTranslations(parentCulture.ToString(), _resourceKey).ForAll(ls =>
                    localizedStrings.Add(new LocalizedString(ls.Key, ls.Text)));
            }

            return localizedStrings;
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
