using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.RegionDomain.Commands;

namespace Orso.Arpa.Application.RegionApplication.Model
{
    public class RegionModifyDto : IdFromRouteDto<RegionModifyBodyDto>
    {
    }

    public class RegionModifyBodyDto
    {
        public string Name { get; set; }
        public bool IsForPerformance { get; set; }
        public bool IsForRehearsal { get; set; }
    }

    public class RegionModifyDtoMappingProfile : Profile
    {
        public RegionModifyDtoMappingProfile()
        {
            CreateMap<RegionModifyDto, ModifyRegion.Command>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Body.Name))
                .ForMember(dest => dest.IsForPerformance, opt => opt.MapFrom(src => src.Body.IsForPerformance))
                .ForMember(dest => dest.IsForRehearsal, opt => opt.MapFrom(src => src.Body.IsForRehearsal));
        }
    }

    public class RegionModifyDtoValidator : IdFromRouteDtoValidator<RegionModifyDto, RegionModifyBodyDto>
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
                .PlaceName(200);
        }
    }
}
