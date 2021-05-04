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
    public static class TranslationSeedData
    {
        public static IList<Translation> Translations
        {
            get
            {
                IList<Translation> result = new List<Translation>();

                try
                {
                    // English
                    string babelEnGbPath = Directory.GetCurrentDirectory() +
                                         "/../Orso.Arpa.Persistence/Seed/Translations/Babel/en-GB.json";
                    string arpaEnGbPath = Directory.GetCurrentDirectory() +
                                        "/../Orso.Arpa.Persistence/Seed/Translations/Arpa/en-GB.json";

                    string babelEnGbJson = File.ReadAllText(babelEnGbPath);
                    IList<Translation> enBabel = ParseBabelTranslations(babelEnGbJson, "en,en-GB");

                    string arpaEnGbJson= File.ReadAllText(arpaEnGbPath);
                    IList<Translation> enGbArpa = ParseArpaTranslations(arpaEnGbJson);

                    IList<Translation> enGbMerge = MergeBabelToArpa(enBabel, enGbArpa);
                    string mergeEnJson = JsonSerializer.Serialize(enGbMerge,
                        new JsonSerializerOptions {WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
                    File.WriteAllText(arpaEnGbPath, mergeEnJson);

                    enGbMerge.ForAll(e => result.Add(e));

                    // German
                    string babelDeDePath = Directory.GetCurrentDirectory() +
                                         "/../Orso.Arpa.Persistence/Seed/Translations/Babel/de-DE.json";
                    string arpaDeDePath = Directory.GetCurrentDirectory() +
                                        "/../Orso.Arpa.Persistence/Seed/Translations/Arpa/de-DE.json";

                    string babelDeDeJson = File.ReadAllText(babelDeDePath);
                    IList<Translation> deDeBabel = ParseBabelTranslations(babelDeDeJson, "de,de-DE");

                    string arpaDeDeJson= File.ReadAllText(arpaDeDePath);
                    IList<Translation> deDeArpa = ParseArpaTranslations(arpaDeDeJson);

                    IList<Translation> deDeMerge = MergeBabelToArpa(deDeBabel, deDeArpa);
                    string mergeDeDeJson = JsonSerializer.Serialize(deDeMerge,
                        new JsonSerializerOptions {WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping});
                    File.WriteAllText(arpaDeDePath, mergeDeDeJson);

                    deDeMerge.ForAll(e => result.Add(e));

                } catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine("Please make sure that you start the migration from Orso.Arpa.Api project directory");
                    throw;
                }

                return result;
            }
        }

        private static IList<Translation> ParseBabelTranslations(string json, string culture)
        {
            IList<Translation> translations = new List<Translation>();

            var jsonDocument = JsonDocument.Parse(json);
            JsonElement rootElement = jsonDocument.RootElement;
            rootElement.EnumerateObject().ForAll(resourceKey =>
            {
                resourceKey.Value.EnumerateObject().ForAll(e =>
                {
                    translations.Add(new Translation(null, e.Name, e.Value.GetString(), culture, resourceKey.Name));
                });
            });

            return translations;
        }

        private static IList<Translation> ParseArpaTranslations(string json)
        {
            return JsonSerializer.Deserialize<IList<Translation>>(json);
        }

        private static IList<Translation> MergeBabelToArpa(IList<Translation> babel,
            IList<Translation> arpa)
        {
            IList<Translation> result = new List<Translation>();

            arpa.ForAll(e => result.Add(e));
            babel.ForAll(b =>
            {
                IQueryable<Translation> query = result.AsQueryable().Where(a =>
                    a.ResourceKey.Equals(b.ResourceKey) && a.Key.Equals(b.Key) && b.Deleted == false);
                if (query.IsNullOrEmpty())
                {
                    result.Add(b);
                }
                else
                {
                    query.ToArray().ForAll(e =>
                    {
                        e.Text = b.Text;
                        e.LocalizationCulture = b.LocalizationCulture;
                        e.LocalizationCulture = b.LocalizationCulture;
                    });
                }
            });

            return result;
        }
    }
}
