using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.TranslationApplication.Services;
using Orso.Arpa.Domain.LocalizationDomain.Model;

namespace Orso.Arpa.Application.TranslationApplication.Model
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
