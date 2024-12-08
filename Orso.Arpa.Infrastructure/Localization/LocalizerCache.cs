using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Orso.Arpa.Domain.General.Interfaces;

namespace Orso.Arpa.Infrastructure.Localization
{
    public class LocalizerCache : ILocalizerCache
    {
        private readonly IServiceCollection _services;
        private readonly object _syncLock = new();
        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> _localizations = [];

        public LocalizerCache(IServiceCollection services)
        {
            _services = services;
        }

        public Task LoadTranslations()
        {
            return Task.Run(() => _localizations = GetDbLocalizationList());
        }

        public bool TryGetTranslation(string key, string resourceKey, string culture, out string translatedString)
        {
            translatedString = key;

            if (_localizations.Count == 0)
            {
                _localizations = GetDbLocalizationList();
            }

            if (_localizations.TryGetValue(resourceKey, out Dictionary<string, Dictionary<string, string>> languageDict)
                && languageDict.TryGetValue(culture.ToLowerInvariant(), out Dictionary<string, string> keyDict)
                && keyDict.TryGetValue(key, out var text)
                && text != null)
            {
                translatedString = text;
                return true;
            }

            return false;
        }

        public virtual Dictionary<string, string> GetAllTranslations(string resourceKey, string culture)
        {
            if (_localizations.TryGetValue(resourceKey, out Dictionary<string, Dictionary<string, string>> languageDict)
                && languageDict.TryGetValue(culture.ToLowerInvariant(), out Dictionary<string, string> keyDict))
            {
                return keyDict;

            }
            return [];
        }

        private Dictionary<string, Dictionary<string, Dictionary<string, string>>> GetDbLocalizationList()
        {
            lock (_syncLock)
            {
                using IServiceScope scope = _services.BuildServiceProvider().CreateScope();
                IArpaContext context = scope.ServiceProvider.GetService<IArpaContext>();
                return new Dictionary<string, Dictionary<string, Dictionary<string, string>>>(from item in context?.Localizations.ToList()
                                                                                              group item by item.ResourceKey into resourceKeyGroup
                                                                                              orderby resourceKeyGroup.Key
                                                                                              select new KeyValuePair<string, Dictionary<string, Dictionary<string, string>>>(
                                                                                                  resourceKeyGroup.Key,
                                                                                                  new Dictionary<string, Dictionary<string, string>>(from foo in resourceKeyGroup
                                                                                                                                                     group foo by foo.LocalizationCulture.ToLowerInvariant() into fooGroup
                                                                                                                                                     select new KeyValuePair<string, Dictionary<string, string>>(fooGroup.Key, fooGroup.ToDictionary(f => f.Key, f => f.Text)))));
            }
        }
    }
}
