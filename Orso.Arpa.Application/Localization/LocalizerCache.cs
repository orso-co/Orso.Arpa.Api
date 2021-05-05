using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.Extensions.DependencyInjection;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Interfaces;

namespace Orso.Arpa.Application.Localization
{
    public class LocalizerCache
    {
        private static IServiceCollection _services;
        private static readonly object _syncLock = new();
        private static List<Translation> _translations;

        public LocalizerCache(IServiceCollection services)
        {
            _services = services;
        }

        public Task CallBack()
        {
            return Task.Run(() => _translations = GetDbTranslationList());
        }

        public string GetTranslation(string key, string resourceKey, string culture)
        {

            if (_translations.IsNullOrEmpty())
            {
                _translations = GetDbTranslationList();
                return key;
            }

            Translation translation = _translations.AsQueryable().DefaultIfEmpty(null).FirstOrDefault(d =>
                d.Deleted == false && d.Key == key && d.ResourceKey == resourceKey && Regex.Match(d.LocalizationCulture, "(^|[^-])"+culture+"([^-]|$)", RegexOptions.None).Success);

            return (translation == null) ? key : translation.Text ?? key;
        }

        public virtual IList<Translation> GetAllTranslations(string culture, string resourceKey)
        {
            if (_translations.IsNullOrEmpty())
                return new List<Translation>();

            IQueryable<Translation> query = _translations.AsQueryable().Where(d =>
                d.Deleted == false && d.ResourceKey.Equals(resourceKey) && Regex.Match(d.LocalizationCulture, "(^|[^-])"+culture+"([^-]|$)", RegexOptions.None).Success);

            return query.IsNullOrEmpty() ? new List<Translation>() : query.ToList();
        }

        private List<Translation> GetDbTranslationList()
        {
            lock (_syncLock)
            {
                using IServiceScope scope = _services.BuildServiceProvider().CreateScope();
                IArpaContext context = scope.ServiceProvider.GetService<IArpaContext>();
                return context?.Translations?.ToList();
            }
        }
    }
}
