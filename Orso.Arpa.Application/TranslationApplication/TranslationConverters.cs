using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Internal;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.TranslationApplication
{

    public class TranslationToLocalizationConverter : ITypeConverter<TranslationDto, IList<Localization>>
    {
        public IList<Localization> Convert(TranslationDto source, IList<Localization> destination, ResolutionContext context)
        {
            IList<Localization> translations = new List<Localization>();

            source.ForAll(rk =>
            {
                rk.Value.ForAll(t =>
                {
                    var localization = new Localization(null, t.Key, t.Value, null, rk.Key);
                    translations.Add(localization);
                });
            });

            return translations;
        }
    }

    public class LocalizationToTranslationConverter : ITypeConverter<IList<Localization>, TranslationDto>
    {
        public TranslationDto Convert(IList<Localization> source, TranslationDto destination, ResolutionContext context)
        {
            TranslationDto root = new();

            source.ForAll(t =>
            {
                if (!root.ContainsKey(t.ResourceKey))
                {
                    root.Add(t.ResourceKey, new ());
                }

                root.TryGetValue(t.ResourceKey, out Dictionary<string, string> entries);
                if (entries!.ContainsKey(t.Key))
                {
                    entries.Remove(t.Key);
                }
                entries!.Add(t.Key, t.Text);
            });

            return root;
        }
    }
}
