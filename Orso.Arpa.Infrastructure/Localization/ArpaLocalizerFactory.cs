using System;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace Orso.Arpa.Infrastructure.Localization
{
    public class ArpaLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ILocalizerCache _cache;

        public ArpaLocalizerFactory (ILocalizerCache cache)
        {
            _cache = cache;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            string culture = CultureInfo.CurrentUICulture.Name;
            return Create(resourceSource.Name, culture);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new StringLocalizer(baseName, location, _cache);
        }
    }
}