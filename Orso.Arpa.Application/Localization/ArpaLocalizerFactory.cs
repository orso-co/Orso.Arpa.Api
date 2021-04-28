using System;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace Orso.Arpa.Application.Localization
{
    public class ArpaLocalizerFactory : IStringLocalizerFactory
    {
        private readonly LocalizerCache _cache;

        public ArpaLocalizerFactory (LocalizerCache cache)
        {
            _cache = cache;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            string culture = CultureInfo.CurrentUICulture.Name ?? "en-US";
            return Create(resourceSource.Name, culture);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new StringLocalizer(baseName, location, _cache);
        }
    }
}
