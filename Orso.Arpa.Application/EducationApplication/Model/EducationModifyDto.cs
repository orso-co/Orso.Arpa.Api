using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.Educations.Modify;

namespace Orso.Arpa.Application.EducationApplication
{
    public class EducationModifyDto : IdFromRouteDto<EducationModifyBodyDto>
    {
    }

    public class EducationModifyBodyDto
    {
        public string TimeSpan { get; set; }
        public string Institution { get; set; }
        public Guid? TypeId { get; set; }
        public string Description { get; set; }
        public byte? SortOrder { get; set; }
    }

    public class EducationModifyDtoMappingProfile : Profile
    {
        public EducationModifyDtoMappingProfile()
        {
            CreateMap<EducationModifyDto, Command>()
                .ForMember(dest => dest.TimeSpan, opt => opt.MapFrom(src => src.Body.TimeSpan))
                .ForMember(dest => dest.Institution, opt => opt.MapFrom(src => src.Body.Institution))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Body.TypeId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
                .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.Body.SortOrder));
        }
    }

    public class EducationModifyDtoValidator : IdFromRouteDtoValidator<EducationModifyDto, EducationModifyBodyDto>
    {
        public EducationModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new EducationModifyBodyDtoValidator());
        }
    }

    public class EducationModifyBodyDtoValidator : AbstractValidator<EducationModifyBodyDto>
    {
        public EducationModifyBodyDtoValidator()
        {
            RuleFor(p => p.TimeSpan)
                .NotEmpty()
                .RestrictedFreeText(50);

            RuleFor(p => p.Institution)
                .NotEmpty()
                .PlaceName(255);

            RuleFor(p => p.Description)
                .RestrictedFreeText(500);
        }
    }
}
