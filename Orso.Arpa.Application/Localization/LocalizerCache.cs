using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.Extensions.DependencyInjection;
using Orso.Arpa.Domain;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Misc;
using Orso.Arpa.Persistence.DataAccess;

namespace Orso.Arpa.Application.Localization
{
    public class LocalizerCache
    {
        private readonly IServiceProvider _serviceProvider;
        private static readonly object _syncLock = new();
        private static List<Translation> _translations = null;

        public LocalizerCache(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _translations = GetDbTranslationList();
        }

        public Task CallBack()
        {
            return Task.Run(() => _translations = GetDbTranslationList());
        }

        public string GetTranslation(string key, string resourceKey, string culture)
        {

            if (_translations.IsNullOrEmpty())
            {
                return key;
            }

            Translation translation = _translations.AsQueryable().First(d =>
                d.Deleted == false && d.Key == key && d.LocalizationCulture == culture);

            return (translation == null) ? key : translation.Text ?? key;
        }

        private List<Translation> GetDbTranslationList()
        {
            lock (_syncLock)
            {
                using IServiceScope scope = _serviceProvider.CreateScope();
                ArpaContext context = scope.ServiceProvider.GetService<ArpaContext>();
                return context?.Translations.ToList();
            }
        }
    }
}
