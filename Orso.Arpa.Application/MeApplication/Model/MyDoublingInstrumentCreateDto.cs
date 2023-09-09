using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;

namespace Orso.Arpa.Application.MeApplication.Model
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
            CreateMap<MyDoublingInstrumentCreateDto, CreateMusicianProfileSection.Command>()
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.Body.LevelAssessmentInner))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.Body.InstrumentId))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Body.Comment))
                .ForMember(dest => dest.AvailabilityId, opt => opt.MapFrom(src => src.Body.AvailabilityId));

            CreateMap<MyDoublingInstrumentCreateBodyDto, CreateMusicianProfileSection.Command>();
        }
    }

    public class MyDoublingInstrumentCreateDtoValidator : IdFromRouteDtoValidator<MyDoublingInstrumentCreateDto, MyDoublingInstrumentCreateBodyDto>
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
                .RestrictedFreeText(500);

            RuleFor(dto => dto.InstrumentId)
                .NotEmpty();

            RuleFor(dto => dto.LevelAssessmentInner)
                .FiveStarRating();
        }
    }
}
