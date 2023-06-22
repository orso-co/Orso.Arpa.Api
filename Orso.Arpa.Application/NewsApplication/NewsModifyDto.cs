using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.News.Modify;

namespace Orso.Arpa.Application.NewsApplication
{
    public class NewsModifyDto : IdFromRouteDto<NewsModifyBodyDto>
    {
    }

    public class NewsModifyBodyDto
    {
        public string NewsTitle { get; set; }
        public string NewsText { get; set; }
        public string Url { get; set; }
        public bool Show { get; set; }
    }

    public class NewsModifyDtoMappingProfile : Profile
    {
        public NewsModifyDtoMappingProfile()
        {
            CreateMap<NewsModifyDto, Command>()
                .ForMember(dest => dest.NewsTitle, opt => opt.MapFrom(src => src.Body.NewsTitle))
                .ForMember(dest => dest.NewsText, opt => opt.MapFrom(src => src.Body.NewsText))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Body.Url))
                .ForMember(dest => dest.Show, opt => opt.MapFrom(src => src.Body.Show))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
    public class NewsModifyDtoValidator : IdFromRouteDtoValidator<NewsModifyDto, NewsModifyBodyDto>
    {
        public NewsModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new NewsModifyBodyDtoValidator());
        }
    }

    public class NewsModifyBodyDtoValidator : AbstractValidator<NewsModifyBodyDto>
    {
        public NewsModifyBodyDtoValidator()
        {
            RuleFor(c => c.NewsTitle)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .RestrictedFreeText(200);
            RuleFor(c => c.NewsText)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .FreeText(1000);

            RuleFor(c => c.Url)
                .ValidUri(1000);
        }
    }
}
