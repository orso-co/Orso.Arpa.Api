using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Logic.MusicianProfileSections;

namespace Orso.Arpa.Application.DoublingInstrumentApplication
{
    public class DoublingInstrumentCreateDto : IdFromRouteDto<DoublingInstrumentCreateBodyDto>
    {
    }

    public class DoublingInstrumentCreateBodyDto
    {
        public Guid InstrumentId { get; set; }
        public byte LevelAssessmentInner { get; set; }
        public byte LevelAssessmentTeam { get; set; }
        public Guid? AvailabilityId { get; set; }
        public string Comment { get; set; }
    }

    public class DoublingInstrumentCreateDtoMappingProfile : Profile
    {
        public DoublingInstrumentCreateDtoMappingProfile()
        {
            CreateMap<DoublingInstrumentCreateDto, Create.Command>()
                .ForMember(dest => dest.MusicianProfileId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LevelAssessmentTeam, opt => opt.MapFrom(src => src.Body.LevelAssessmentTeam))
                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.Body.LevelAssessmentInner))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.Body.InstrumentId))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Body.Comment))
                .ForMember(dest => dest.AvailabilityId, opt => opt.MapFrom(src => src.Body.AvailabilityId));

            CreateMap<DoublingInstrumentCreateBodyDto, Create.Command>();
        }
    }

    public class DoublingInstrumentCreateDtoValidator : IdFromRouteDtoValidator<DoublingInstrumentCreateDto, DoublingInstrumentCreateBodyDto>
    {
        public DoublingInstrumentCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new DoublingInstrumentCreateBodyDtoValidator());
        }
    }

    public class DoublingInstrumentCreateBodyDtoValidator : AbstractValidator<DoublingInstrumentCreateBodyDto>
    {
        public DoublingInstrumentCreateBodyDtoValidator()
        {
            RuleFor(dto => dto.Comment)
                .MaximumLength(500);

            RuleFor(dto => dto.InstrumentId)
                .NotEmpty();

            RuleFor(dto => dto.LevelAssessmentInner)
                .FiveStarRating();

            RuleFor(dto => dto.LevelAssessmentTeam)
                .FiveStarRating();
        }
    }
}
