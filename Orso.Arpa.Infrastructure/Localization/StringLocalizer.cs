using System.Collections.Generic;
using System.Globalization;
using AutoMapper.Internal;
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

            _cache.GetAllTranslations(_resourceKey, _location).ForAll(ls =>
                localizedStrings.Add(new LocalizedString(ls.Key, ls.Text)));

            if (includeParentCultures)
            {
                _cache.GetAllTranslations(_resourceKey, CultureInfo.GetCultureInfo(_location).Parent.ToString()).ForAll(ls =>
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