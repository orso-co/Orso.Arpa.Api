using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.RegionDomain.Commands;

namespace Orso.Arpa.Application.RegionApplication.Model
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
            CreateMap<RegionCreateDto, CreateRegion.Command>();
        }
    }

    public class RegionCreateDtoValidator : AbstractValidator<RegionCreateDto>
    {
        public RegionCreateDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .PlaceName(200);
        }
    }
}
