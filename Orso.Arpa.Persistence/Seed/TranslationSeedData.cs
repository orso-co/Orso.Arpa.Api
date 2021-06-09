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
                    // English
                    string babelEnGbPath = Directory.GetCurrentDirectory() +
                                         "/../Orso.Arpa.Persistence/Seed/Translations/Translation/en-GB.json";
                    string arpaEnGbPath = Directory.GetCurrentDirectory() +
                                        "/../Orso.Arpa.Persistence/Seed/Translations/Localization/en-GB.json";

                    string babelEnGbJson = File.ReadAllText(babelEnGbPath);
                    IList<Localization> enBabel = ParseBabelTranslations(babelEnGbJson, "en-GB");

                    string arpaEnGbJson= File.ReadAllText(arpaEnGbPath);
                    IList<Localization> enGbArpa = ParseArpaTranslations(arpaEnGbJson);

                    IList<Localization> enGbMerge = MergeBabelToArpa(enBabel, enGbArpa);
                    string mergeEnJson = JsonSerializer.Serialize(enGbMerge,
                        new JsonSerializerOptions {WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
                    File.WriteAllText(arpaEnGbPath, mergeEnJson);

                    enGbMerge.ForAll(e => result.Add(e));

                    // German
                    string babelDeDePath = Directory.GetCurrentDirectory() +
                                         "/../Orso.Arpa.Persistence/Seed/Translations/Translation/de-DE.json";
                    string arpaDeDePath = Directory.GetCurrentDirectory() +
                                        "/../Orso.Arpa.Persistence/Seed/Translations/Localization/de-DE.json";

                    string babelDeDeJson = File.ReadAllText(babelDeDePath);
                    IList<Localization> deDeBabel = ParseBabelTranslations(babelDeDeJson, "de-DE");

                    string arpaDeDeJson= File.ReadAllText(arpaDeDePath);
                    IList<Localization> deDeArpa = ParseArpaTranslations(arpaDeDeJson);

                    IList<Localization> deDeMerge = MergeBabelToArpa(deDeBabel, deDeArpa);
                    string mergeDeDeJson = JsonSerializer.Serialize(deDeMerge,
                        new JsonSerializerOptions {WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
                    File.WriteAllText(arpaDeDePath, mergeDeDeJson);

                    deDeMerge.ForAll(e => result.Add(e));

                } catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Please make sure that you start the migration from Orso.Arpa.Api project directory");
                }

                return result;
            }
        }

        private static IList<Localization> ParseBabelTranslations(string json, string culture)
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

        private static IList<Localization> ParseArpaTranslations(string json)
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

        private static IList<Localization> MergeBabelToArpa(IList<Localization> babel,
            IList<Localization> arpa)
        {
            IList<Localization> result = new List<Localization>();

            // Check already existing arpa translations and update
            arpa.AsQueryable().Where(a => a.Deleted == false).ForAll(a =>
            {
                IQueryable<Localization> query = babel.AsQueryable().Where(b =>
                    a.ResourceKey.Equals(b.ResourceKey) && a.Key.Equals(b.Key));

                if (query.IsNullOrEmpty())  // if entry was removed.
                {
                    if (a.Deleted == false)
                        a.Delete(nameof(LocalizationSeedData), DateTime.Now);
                    result.Add(a);
                }
                else
                {   // if entry can be found in babel json
                    Localization babelTranslate = query.First();
                    Localization updatedLocalization = new Localization(a.Id, babelTranslate.Key,
                        babelTranslate.Text, babelTranslate.LocalizationCulture,
                        babelTranslate.ResourceKey);
                    updatedLocalization.Create(a.CreatedBy, a.CreatedAt);

                    // then check whether text changed.
                    if (!a.Text.Equals(babelTranslate.Text))
                    {
                        updatedLocalization.Modify(nameof(LocalizationSeedData), DateTime.Now);
                    }

                    result.Add(updatedLocalization);
                }
            });

            // Check for new entries
            babel.ForAll(b =>
            {
                IQueryable<Localization> query = arpa.AsQueryable().Where(a =>
                    a.ResourceKey.Equals(b.ResourceKey) && a.Key.Equals(b.Key) &&
                    a.Deleted == false);

                if (query.IsNullOrEmpty())
                {
                    Localization newLocalization = new Localization(b.Id, b.Key, b.Text,
                        b.LocalizationCulture, b.ResourceKey);

                    newLocalization.Create(nameof(LocalizationSeedData), DateTime.Now);

                    result.Add(newLocalization);
                }
            });

            return result;
        }
    }
}
