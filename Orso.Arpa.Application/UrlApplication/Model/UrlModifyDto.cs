using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.ProjectDomain.Commands;

namespace Orso.Arpa.Application.UrlApplication.Model
{
    public class UrlModifyDto : IdFromRouteDto<UrlModifyBodyDto>
    {
    }

    public class UrlModifyBodyDto
    {
        public string Href { get; set; }
        public string AnchorText { get; set; }
    }

    public class UrlModifyDtoMappingProfile : Profile
    {
        public UrlModifyDtoMappingProfile()
        {
            CreateMap<UrlModifyDto, ModifyUrl.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Href, opt => opt.MapFrom(src => src.Body.Href))
                .ForMember(dest => dest.AnchorText, opt => opt.MapFrom(src => src.Body.AnchorText));
        }
    }

    public class UrlModifyDtoValidator : IdFromRouteDtoValidator<UrlModifyDto, UrlModifyBodyDto>
    {
        public UrlModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new UrlModifyBodyDtoValidator());
        }
    }

    public class UrlModifyBodyDtoValidator : AbstractValidator<UrlModifyBodyDto>
    {
        public UrlModifyBodyDtoValidator()
        {
            RuleFor(p => p.Href)
               .ValidUri(1000);

            RuleFor(p => p.AnchorText)
                .RestrictedFreeText(1000);
        }
    }
}
