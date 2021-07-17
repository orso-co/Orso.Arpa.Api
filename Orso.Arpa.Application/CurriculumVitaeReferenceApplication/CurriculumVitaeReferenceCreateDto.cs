using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.CurriculumVitaeReferences.Create;

namespace Orso.Arpa.Application.CurriculumVitaeReferenceApplication
{
    public class CurriculumVitaeReferenceCreateDto : IdFromRouteDto<CurriculumVitaeReferenceCreateBodyDto>
    {
    }

    public class CurriculumVitaeReferenceCreateBodyDto
    {
        public string TimeSpan { get; set; }
        public string Institution { get; set; }
        public Guid? TypeId { get; set; }
        public string Description { get; set; }
        public byte? SortOrder { get; set; }

    }

    public class CurriculumVitaeReferenceCreateDtoMappingProfile : Profile
    {
        public CurriculumVitaeReferenceCreateDtoMappingProfile()
        {
            CreateMap<CurriculumVitaeReferenceCreateDto, Command>()
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TimeSpan, opt => opt.MapFrom(src => src.Body.TimeSpan))
                .ForMember(dest => dest.Institution, opt => opt.MapFrom(src => src.Body.Institution))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Body.TypeId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
                .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.Body.SortOrder));
        }
    }

    public class CurriculumVitaeReferenceCreateDtoValidator : IdFromRouteDtoValidator<CurriculumVitaeReferenceCreateDto, CurriculumVitaeReferenceCreateBodyDto>
    {
        public CurriculumVitaeReferenceCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new CurriculumVitaeReferenceCreateBodyDtoValidator());
        }
    }

    public class CurriculumVitaeReferenceCreateBodyDtoValidator : AbstractValidator<CurriculumVitaeReferenceCreateBodyDto>
    {
        public CurriculumVitaeReferenceCreateBodyDtoValidator()
        {
            RuleFor(p => p.TimeSpan)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(p => p.Institution)
                .MaximumLength(255);

            RuleFor(p => p.Description)
                .MaximumLength(500);
        }
    }
}
