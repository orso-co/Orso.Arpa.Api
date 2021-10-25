using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.Urls.Create;

namespace Orso.Arpa.Application.UrlApplication
{
    public class UrlCreateDto : IdFromRouteDto<UrlCreateBodyDto>
    {
    }

    public class UrlCreateBodyDto
    {
        public string Href { get; set; }
        public string AnchorText { get; set; }
    }

    public class UrlCreateDtoMappingProfile : Profile
    {
        public UrlCreateDtoMappingProfile()
        {
            CreateMap<UrlCreateDto, Command>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Href, opt => opt.MapFrom(src => src.Body.Href))
                .ForMember(dest => dest.AnchorText, opt => opt.MapFrom(src => src.Body.AnchorText));
        }
    }

    public class UrlCreateDtoValidator : IdFromRouteDtoValidator<UrlCreateDto, UrlCreateBodyDto>
    {
        public UrlCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new UrlCreateBodyDtoValidator());
        }
    }

    public class UrlCreateBodyDtoValidator : AbstractValidator<UrlCreateBodyDto>
    {
        public UrlCreateBodyDtoValidator()
        {
            RuleFor(p => p.Href)
                 .ValidUri(1000);

            RuleFor(p => p.AnchorText)
                .PlaceName(1000);
        }
    }
}
