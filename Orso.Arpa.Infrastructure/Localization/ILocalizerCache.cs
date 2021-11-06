using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orso.Arpa.Infrastructure.Localization
{
    public interface ILocalizerCache
    {
        /// <summary>
        /// Return the translation for the given <paramref name="key"/>, <paramref name="resourceKey"/> and <paramref name="culture"/>.
        /// </summary>
        /// <param name="key">The key for which to load the translation.</param>
        /// <param name="resourceKey">Indicates for which resource to load the translation for.</param>
        /// <param name="culture">The culture for which to load the translation.</param>
        /// <returns>The translation for the combination of the given parameters.</returns>
        public bool TryGetTranslation(string key, string resourceKey, string culture, out string translatedString);

        /// <summary>
        /// Returns all translations for the given resourceKey and culture.
        /// </summary>
        /// <param name="resourceKey">Indicates for which resource to load the translation for.</param>
        /// <param name="culture">The culture for which to load the translation.</param>
        /// <returns>A list of <see cref="Localization"/> matching <paramref name="resourceKey"/> and <paramref name="culture"/>.</returns>
        Dictionary<string, string> GetAllTranslations(string resourceKey, string culture);

        /// <summary>
        /// Loads translations from DB into this cache.
        /// </summary>
        /// <returns><see cref="Task"/> object</returns>
        public Task LoadTranslations();
    }
}
