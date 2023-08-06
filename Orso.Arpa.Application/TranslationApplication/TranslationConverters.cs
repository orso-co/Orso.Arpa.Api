using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.TranslationApplication
{

    public class TranslationToLocalizationConverter : ITypeConverter<TranslationDto, List<Localization>>
    {
        public List<Localization> Convert(TranslationDto source, List<Localization> destination, ResolutionContext context)
        {
            var translations = new List<Localization>();

            foreach (KeyValuePair<string, Dictionary<string, string>> rk in source)
            {
                foreach (KeyValuePair<string, string> t in rk.Value)
                {
                    var localization = new Localization(null, t.Key, t.Value, null, rk.Key);
                    translations.Add(localization);
                }
            }

            return translations;
        }
    }

    public class LocalizationToTranslationConverter : ITypeConverter<List<Localization>, TranslationDto>
    {
        public TranslationDto Convert(List<Localization> source, TranslationDto destination, ResolutionContext context)
        {
            TranslationDto root = new();

            foreach (Localization t in source)
            {
                if (!root.ContainsKey(t.ResourceKey))
                {
                    root.Add(t.ResourceKey, new());
                }

                root.TryGetValue(t.ResourceKey, out Dictionary<string, string> entries);
                entries.Remove(t.Key);
                entries!.Add(t.Key, t.Text);
            }

            return root;
        }
    }
}
