using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Infrastructure.Localization
{
    public class LocalizerCache : ILocalizerCache
    {
        private static IServiceCollection _services;
        private static readonly object _syncLock = new();
        private static List<Domain.Entities.Localization> _localizations = new List<Domain.Entities.Localization>();

        public LocalizerCache(IServiceCollection services)
        {
            _services = services;
        }

        public Task LoadTranslations()
        {
            return Task.Run(() => _localizations = GetDbLocalizationList());
        }

        public string GetTranslation(string key, string resourceKey, string culture)
        {

            if (_localizations.Count == 0)
            {
                _localizations = GetDbLocalizationList();
                return key;
            }

            Domain.Entities.Localization localization = _localizations.AsQueryable().DefaultIfEmpty(null).FirstOrDefault(d =>
                d.Key.Equals(key) && d.ResourceKey.Equals(resourceKey) && d.LocalizationCulture.Equals(culture, StringComparison.InvariantCultureIgnoreCase));

            return (localization == null) ? key : localization.Text ?? key;
        }

        public virtual IList<Domain.Entities.Localization> GetAllTranslations(string resourceKey, string culture)
        {
            if (_localizations.Count == 0)
            {
                return new List<Domain.Entities.Localization>();
            }

            IQueryable<Domain.Entities.Localization> query = _localizations.AsQueryable().Where(d =>
                d.ResourceKey.Equals(resourceKey) && d.LocalizationCulture.Equals(culture, StringComparison.InvariantCultureIgnoreCase));

            return !query.Any() ? new List<Domain.Entities.Localization>() : query.ToList();
        }

        private List<Domain.Entities.Localization> GetDbLocalizationList()
        {
            lock (_syncLock)
            {
                using IServiceScope scope = _services.BuildServiceProvider().CreateScope();
                IArpaContext context = scope.ServiceProvider.GetService<IArpaContext>();
                return context?.Localizations?.AsQueryable().Where(t => !t.Deleted).ToList();
            }
        }
    }
}
