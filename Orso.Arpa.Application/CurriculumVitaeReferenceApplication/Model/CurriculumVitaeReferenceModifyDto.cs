using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;

namespace Orso.Arpa.Application.CurriculumVitaeReferenceApplication.Model
{
    public class CurriculumVitaeReferenceModifyDto : IdFromRouteDto<CurriculumVitaeReferenceModifyBodyDto>
    {
    }

    public class CurriculumVitaeReferenceModifyBodyDto
    {
        public string TimeSpan { get; set; }
        public string Institution { get; set; }
        public Guid? TypeId { get; set; }
        public string Description { get; set; }
        public byte? SortOrder { get; set; }
    }

    public class CurriculumVitaeReferenceModifyDtoMappingProfile : Profile
    {
        public CurriculumVitaeReferenceModifyDtoMappingProfile()
        {
            CreateMap<CurriculumVitaeReferenceModifyDto, ModifyCurriculumVitaeReference.Command>()
                .ForMember(dest => dest.TimeSpan, opt => opt.MapFrom(src => src.Body.TimeSpan))
                .ForMember(dest => dest.Institution, opt => opt.MapFrom(src => src.Body.Institution))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Body.TypeId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
                .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.Body.SortOrder));
        }
    }

    public class CurriculumVitaeReferenceModifyDtoValidator : IdFromRouteDtoValidator<CurriculumVitaeReferenceModifyDto, CurriculumVitaeReferenceModifyBodyDto>
    {
        public CurriculumVitaeReferenceModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new CurriculumVitaeReferenceModifyBodyDtoValidator());
        }
    }

    public class CurriculumVitaeReferenceModifyBodyDtoValidator : AbstractValidator<CurriculumVitaeReferenceModifyBodyDto>
    {
        public CurriculumVitaeReferenceModifyBodyDtoValidator()
        {
            RuleFor(p => p.TimeSpan)
                .NotEmpty()
                .RestrictedFreeText(50);

            RuleFor(p => p.Institution)
                .PlaceName(255);

            RuleFor(p => p.Description)
                .RestrictedFreeText(500);
        }
    }
}
