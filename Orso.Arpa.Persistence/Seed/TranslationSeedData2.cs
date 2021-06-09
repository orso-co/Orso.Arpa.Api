using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using AutoMapper.Internal;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Seed
{
    public static class TranslationSeedData2
    {
        private static IList<Localization> ParseLocalizationJson(string json)
        {
            // Don't even try this: >> JsonSerializer.Deserialize<List<Translation>>(json) <<
            IList<Localization> translations = new List<Localization>();

            var jsonDocument = JsonDocument.Parse(json);
            JsonElement rootElement = jsonDocument.RootElement;
            rootElement.EnumerateArray().ForAll(t =>
            {
                string key = t.TryGetProperty("Key", out JsonElement element) ? element.GetString() : null;
                string text = t.TryGetProperty("Text", out element) ? element.GetString() : null;
                string localizationCulture = t.TryGetProperty("LocalizationCulture", out element)
                    ? element.GetString()
                    : null;
                string resourceKey = t.TryGetProperty("ResourceKey", out element)
                    ? element.GetString()
                    : null;
                Guid id = t.TryGetProperty("Id", out element) ? element.GetGuid() : Guid.Empty;
                string createdBy = t.TryGetProperty("CreatedBy", out element) ? element.GetString() : null;
                DateTime createdAt = t.TryGetProperty("CreatedAt", out element)
                    ? (element.TryGetDateTime(out DateTime cdt) ? cdt : DateTime.Now)
                    : DateTime.Now;
                string modifiedBy = t.TryGetProperty("ModifiedBy", out element)
                    ? element.GetString()
                    : null;
                DateTime? modifiedAt = t.TryGetProperty("ModifiedAt", out element)
                        ? (element.GetString() == null ? null
                    : element.TryGetDateTime(out DateTime mdt) ? mdt : null)
                    : null;
                bool deleted = t.TryGetProperty("Deleted", out element) && element.GetBoolean();

                translations.Add(new Localization(id, key, text, localizationCulture, resourceKey, createdBy, createdAt, modifiedBy, modifiedAt, deleted));
            });
            return translations;
        }

        private  static IList<Localization> UpdateCreateBy(this IList<Localization> input, string createdBy)
        {
            IList<Localization> result = new List<Localization>();

            input.ForAll(i =>
            {
                var localization = new Localization(i.Id, i.Key, i.Text,
                    i.LocalizationCulture, i.ResourceKey, createdBy, i.CreatedAt, i.ModifiedBy,
                    i.ModifiedAt, i.Deleted);

                result.Add(localization);
            });

            return result;
        }

        private  static IList<Localization> UpdateCreateAt(this IList<Localization> input, DateTime createdAt)
        {
            IList<Localization> result = new List<Localization>();

            input.ForAll(i =>
            {
                var localization = new Localization(i.Id, i.Key, i.Text,
                    i.LocalizationCulture, i.ResourceKey, i.CreatedBy, createdAt, i.ModifiedBy,
                    i.ModifiedAt, i.Deleted);

                result.Add(localization);
            });

            return result;
        }

        /// <summary>
        /// Searches an entry by Culture, ResourceKey, and Key and updated it if needed.
        /// if both entries are equal by Culture, ResourceKey, Key and Text then destination entry is returned.
        /// if both entries differ then the newest entry is returned.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private static IList<Localization> UpdateLocalization(IList<Localization> source, IList<Localization> destination)
        {

            IList<Localization> result = new List<Localization>();

            destination.ForAll(d =>
            {
                Localization s = source.AsQueryable().DefaultIfEmpty(null).FirstOrDefault(e =>
                    e.ResourceKey.Equals(d.ResourceKey) &&
                    e.LocalizationCulture.Equals(d.LocalizationCulture) && e.Key.Equals(d.Key));

                if (s == null)      // if source does not contains such an entry.
                {
                    result.Add(d);
                }
                else
                {
                    if (!s.Text.Equals(d.Text)) // if Text changed then check which entry is newer
                    {
                        DateTime sourceAge = s.ModifiedAt ?? s.CreatedAt;
                        DateTime destAge = d.ModifiedAt ?? d.CreatedAt;

                        if (sourceAge > destAge)    // if s is newer
                        {
                            var l = new Localization(d.Id, d.Key, s.Text, d.LocalizationCulture,
                                d.ResourceKey, d.CreatedBy, d.CreatedAt,
                                s.ModifiedBy ?? s.CreatedBy, sourceAge, false);

                            result.Add(l);
                        }
                        else // if d is newer then keep d
                        {
                            result.Add(d);
                        }
                    }
                }
            });

            // Add all new entries that do not exist in destination list.
            source.ForAll(s =>
            {
                Localization l = destination.AsQueryable().DefaultIfEmpty(null).FirstOrDefault(d =>
                    d.LocalizationCulture.Equals(s.LocalizationCulture) &&
                    d.ResourceKey.Equals(s.ResourceKey) && d.Key.Equals(s.Key));

                if (l == null)
                {
                    result.Add(s);
                }
            });

            return result;
        }
    }
}
