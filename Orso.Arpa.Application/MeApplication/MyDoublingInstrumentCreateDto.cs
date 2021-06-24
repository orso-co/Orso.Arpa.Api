using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MyDoublingInstrumentCreateDto : IdFromRouteDto<MyDoublingInstrumentCreateBodyDto> { }

    public class MyDoublingInstrumentCreateBodyDto
    {
        public Guid InstrumentId { get; set; }
        public byte LevelAssessmentInner { get; set; }
        public Guid? AvailabilityId { get; set; }
        public string Comment { get; set; }
    }

    public class MyDoublingInstrumentCreateDtoMappingProfile : Profile
    {
        public MyDoublingInstrumentCreateDtoMappingProfile()
        {
            CreateMap<MyDoublingInstrumentCreateDto, Domain.Logic.MusicianProfileSections.Create.Command>()
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.Body.LevelAssessmentInner))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.Body.InstrumentId))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Body.Comment))
                .ForMember(dest => dest.AvailabilityId, opt => opt.MapFrom(src => src.Body.AvailabilityId));

            CreateMap<MyDoublingInstrumentCreateBodyDto, Domain.Logic.MusicianProfileSections.Create.Command>();
        }
    }

    public class MyDoublingInstrumentCreateDtoValidator : BaseModifyDtoValidator<MyDoublingInstrumentCreateDto, MyDoublingInstrumentCreateBodyDto>
    {
        public MyDoublingInstrumentCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new MyDoublingInstrumentCreateBodyDtoValidator());
        }
    }

    public class MyDoublingInstrumentCreateBodyDtoValidator : AbstractValidator<MyDoublingInstrumentCreateBodyDto>
    {
        public MyDoublingInstrumentCreateBodyDtoValidator()
        {
            RuleFor(dto => dto.Comment)
                .MaximumLength(500);

            RuleFor(dto => dto.InstrumentId)
                .NotEmpty();

            RuleFor(dto => dto.LevelAssessmentInner)
                .FiveStarRating();
        }
    }
}
