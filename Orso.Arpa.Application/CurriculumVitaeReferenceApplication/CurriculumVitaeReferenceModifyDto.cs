using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.CurriculumVitaeReferences.Create;

namespace Orso.Arpa.Application.CurriculumVitaeReferenceApplication
{
    public class CurriculumVitaeReferenceModifyDto : IdFromRouteDto<CurriculumVitaeReferenceModifyBodyDto>
    {
    }

    public class CurriculumVitaeReferenceModifyBodyDto
    {
        public string TimeSpan { get; set; }
        public string Institution { get; set; }
        public Guid TypeId { get; set; }
        public string Description { get; set; }
        public byte SortOrder { get; set; }
    }

    public class CurriculumVitaeReferenceModifyDtoMappingProfile : Profile
    {
        public CurriculumVitaeReferenceModifyDtoMappingProfile()
        {
            CreateMap<CurriculumVitaeReferenceModifyDto, Command>()
                .ForMember(dest => dest.TimeSpan, opt => opt.MapFrom(src => src.Body.TimeSpan))
                .ForMember(dest => dest.Institution, opt => opt.MapFrom(src => src.Body.Institution))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Body.TypeId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
                .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.Body.SortOrder));
        }
    }

    public class CurriculumVitaeReferenceModifyDtoValidator : BaseModifyDtoValidator<CurriculumVitaeReferenceModifyDto, CurriculumVitaeReferenceModifyBodyDto>
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
            RuleFor(p => p.TimeSpan).MaximumLength(50);
            RuleFor(p => p.Institution).MaximumLength(255);
            RuleFor(p => p.TypeId).NotEmpty();
            RuleFor(p => p.Description).MaximumLength(500);
        }
    }
}
