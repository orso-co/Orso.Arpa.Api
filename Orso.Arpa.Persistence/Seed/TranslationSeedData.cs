using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using AutoMapper.Internal;
using Castle.Core.Internal;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Persistence.Seed
{
    public static class LocalizationSeedData
    {
        public static IList<Localization> Localizations
        {
            get
            {
                IList<Localization> result = new List<Localization>();

                try
                {
                    // Default English
                    ApplyTranslation(
                        Directory.GetCurrentDirectory() + "/../Orso.Arpa.Persistence/Seed/Translations/Translation/en.json",
                        Directory.GetCurrentDirectory() + "/../Orso.Arpa.Persistence/Seed/Translations/Localization/en.json",
                        "en").ForAll(e => result.Add(e));

                    // German
                    ApplyTranslation(
                        Directory.GetCurrentDirectory() + "/../Orso.Arpa.Persistence/Seed/Translations/Translation/de.json",
                        Directory.GetCurrentDirectory() + "/../Orso.Arpa.Persistence/Seed/Translations/Localization/de.json",
                        "de").ForAll(e => result.Add(e));
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Please make sure that you start the migration from Orso.Arpa.Api project directory");
                }

                return result;
            }
        }

        private static IList<Localization> ApplyTranslation(
            string translationPath,
            string localizationPath,
            string culture)
        {
            if (translationPath == null)
            {
                throw new ArgumentNullException(nameof(translationPath));
            }

            string translationsJson = File.ReadAllText(translationPath);
            IList<Localization> translationsList = ParseTranslations(translationsJson, culture);

            string localizationsJson = File.ReadAllText(localizationPath);
            IList<Localization> localizationsList = ParseLocalications(localizationsJson);

            IList<Localization> merge = MergeTranslationToLocalication(translationsList, localizationsList);
            string mergeJson = JsonSerializer.Serialize(merge,
                new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });

            File.WriteAllText(localizationPath, mergeJson);

            return merge;
        }

        private static IList<Localization> ParseTranslations(string json, string culture)
        {
            IList<Localization> translations = new List<Localization>();

            var jsonDocument = JsonDocument.Parse(json);
            JsonElement rootElement = jsonDocument.RootElement;
            rootElement.EnumerateObject().ForAll(resourceKey =>
            {
                resourceKey.Value.EnumerateObject().ForAll(e =>
                {
                    translations.Add(new Localization(null, e.Name, e.Value.GetString(), culture, resourceKey.Name));
                });
            });

            return translations;
        }

        private static IList<Localization> ParseLocalications(string json)
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

        private static IList<Localization> MergeTranslationToLocalication(IList<Localization> translations,
            IList<Localization> localizations)
        {
            IList<Localization> result = new List<Localization>();

            // Check already existing arpa translations and update
            localizations.AsQueryable().Where(a => a.Deleted == false).ForAll(a =>
            {
                IQueryable<Localization> query = translations.AsQueryable().Where(b =>
                    a.ResourceKey.Equals(b.ResourceKey) && a.Key.Equals(b.Key));

                if (query.IsNullOrEmpty())  // if entry was removed.
                {
                    if (a.Deleted == false)
                    {
                        a.Delete(nameof(LocalizationSeedData), DateTime.Now);
                    }

                    result.Add(a);
                }
                else
                {   // if entry can be found in babel json
                    Localization translate = query.First();
                    var updatedLocalization = new Localization(a.Id, translate.Key,
                        translate.Text, translate.LocalizationCulture,
                        translate.ResourceKey);
                    updatedLocalization.Create(a.CreatedBy, a.CreatedAt);

                    // then check whether text changed.
                    if (!a.Text.Equals(translate.Text))
                    {
                        updatedLocalization.Modify(nameof(LocalizationSeedData), DateTime.Now);
                    }

                    result.Add(updatedLocalization);
                }
            });

            // Check for new entries
            translations.ForAll(b =>
            {
                IQueryable<Localization> query = localizations.AsQueryable().Where(a =>
                    a.ResourceKey.Equals(b.ResourceKey) && a.Key.Equals(b.Key) &&
                    a.Deleted == false);

                if (query.IsNullOrEmpty())
                {
                    var newLocalization = new Localization(b.Id, b.Key, b.Text,
                        b.LocalizationCulture, b.ResourceKey);

                    newLocalization.Create(nameof(LocalizationSeedData), DateTime.Now);

                    result.Add(newLocalization);
                }
            });

            return result;
        }
    }
}
