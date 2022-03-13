using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.TranslationApplication
{
    public class TranslationDto : Dictionary<string, Dictionary<string, string>>
    {

    }

    public class TranslationDtoProfile : Profile
    {
        public TranslationDtoProfile()
        {
            CreateMap<TranslationDto, List<Localization>>().ConvertUsing<TranslationToLocalizationConverter>();
            CreateMap<List<Localization>, TranslationDto>().ConvertUsing<LocalizationToTranslationConverter>();
        }
    }
}
