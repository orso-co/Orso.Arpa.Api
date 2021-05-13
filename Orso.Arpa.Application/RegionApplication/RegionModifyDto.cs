using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.Regions.Modify;

namespace Orso.Arpa.Application.RegionApplication
{
    public class RegionModifyDto : BaseModifyDto<RegionModifyBodyDto>
    {
    }

    public class RegionModifyBodyDto
    {
        public string Name { get; set; }
    }

    public class RegionModifyDtoMappingProfile : Profile
    {
        public RegionModifyDtoMappingProfile()
        {
            CreateMap<RegionModifyDto, Command>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Body.Name));
        }
    }

    public class RegionModifyDtoValidator : BaseModifyDtoValidator<RegionModifyDto, RegionModifyBodyDto>
    {
        public RegionModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new RegionModifyBodyDtoValidator());
        }
    }

    public class RegionModifyBodyDtoValidator : AbstractValidator<RegionModifyBodyDto>
    {
        public RegionModifyBodyDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
