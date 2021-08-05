using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using static Orso.Arpa.Domain.Logic.Regions.Create;

namespace Orso.Arpa.Application.RegionApplication
{
    public class RegionCreateDto
    {
        public string Name { get; set; }
        public bool IsForRehearsal { get; set; }
        public bool IsForPerformance { get; set; }
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
            RuleFor(c => c.Name)
                .NotEmpty()
                .GeneralText(200);
        }
    }
}
