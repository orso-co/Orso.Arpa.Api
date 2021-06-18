using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.Educations.Create;

namespace Orso.Arpa.Application.EducationApplication
{
    public class EducationCreateDto : IdFromRouteDto<EducationCreateBodyDto>
    {
    }

    public class EducationCreateBodyDto
    {
        public string TimeSpan { get; set; }
        public string Institution { get; set; }
        public Guid TypeId { get; set; }
        public string Description { get; set; }
        public byte SortOrder { get; set; }
    }

    public class EducationCreateDtoMappingProfile : Profile
    {
        public EducationCreateDtoMappingProfile()
        {
            CreateMap<EducationCreateDto, Command>()
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TimeSpan, opt => opt.MapFrom(src => src.Body.TimeSpan))
                .ForMember(dest => dest.Institution, opt => opt.MapFrom(src => src.Body.Institution))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Body.TypeId))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description))
                .ForMember(dest => dest.SortOrder, opt => opt.MapFrom(src => src.Body.SortOrder));
        }
    }

    public class EducationCreateDtoValidator : BaseModifyDtoValidator<EducationCreateDto, EducationCreateBodyDto>
    {
        public EducationCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new EducationCreateBodyDtoValidator());
        }
    }

    public class EducationCreateBodyDtoValidator : AbstractValidator<EducationCreateBodyDto>
    {
        public EducationCreateBodyDtoValidator()
        {
            RuleFor(p => p.TimeSpan).MaximumLength(50);
            RuleFor(p => p.Institution).MaximumLength(255);
            RuleFor(p => p.TypeId).NotEmpty();
            RuleFor(p => p.Description).MaximumLength(500);
        }
    }
}
