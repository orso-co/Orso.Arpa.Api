using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Regions.Create;

namespace Orso.Arpa.Application.RegionApplication
{
    public class RegionCreateDto
    {
        public string Name { get; set; }
    }

    public class RegionCreateDtoMappingProfile : Profile
    {
        public RegionCreateDtoMappingProfile()
        {
            CreateMap<RegionCreateDto, Command>();
        }
    }

    public class RegionCreateDtoValidator : AbstractValidator<RegionCreateDto>
    {
        public RegionCreateDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
