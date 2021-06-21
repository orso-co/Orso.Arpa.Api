using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.MusicianProfiles.Create;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MusicianProfileCreateDto : IdFromRouteDto<MusicianProfileCreateBodyDto>
    {
    }

    public class MusicianProfileCreateBodyDto
    {
        public byte LevelAssessmentInner { get; set; }
        public byte LevelAssessmentTeam { get; set; }
        public Guid InstrumentId { get; set; }
        public Guid QualificationId { get; set; }
        public Guid? InquiryStatusInnerId { get; set; }
        public Guid? InquiryStatusTeamId { get; set; }
        public IList<DoublingInstrumentCreateDto> DoublingInstruments { get; set; } = new List<DoublingInstrumentCreateDto>();
        public IList<Guid> PreferredPositionsInnerIds { get; set; } = new List<Guid>();
        public IList<Guid> PreferredPositionsTeamIds { get; set; } = new List<Guid>();
        public IList<byte> PreferredPartsInner { get; set; } = new List<byte>();
        public IList<byte> PreferredPartsTeam { get; set; } = new List<byte>();
    }

    public class DoublingInstrumentCreateDto
    {
        public Guid InstrumentId { get; set; }
        public byte LevelAssessmentInner { get; set; }
        public byte LevelAssessmentTeam { get; set; }
        public Guid? AvailabilityId { get; set; }
        public string Comment { get; set; }
    }

    public class MusicianProfileCreateDtoMappingProfile : Profile
    {
        public MusicianProfileCreateDtoMappingProfile()
        {
            CreateMap<MusicianProfileCreateDto, Command>()
                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.Body.LevelAssessmentInner))
                .ForMember(dest => dest.LevelAssessmentTeam, opt => opt.MapFrom(src => src.Body.LevelAssessmentTeam))

                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.Body.InstrumentId))
                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.Body.QualificationId))
                .ForMember(dest => dest.InquiryStatusInnerId, opt => opt.MapFrom(src => src.Body.InquiryStatusInnerId))
                .ForMember(dest => dest.InquiryStatusTeamId, opt => opt.MapFrom(src => src.Body.InquiryStatusTeamId))

                .ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.Body.DoublingInstruments))
                .ForMember(dest => dest.PreferredPositionsInnerIds, opt => opt.MapFrom(src => src.Body.PreferredPositionsInnerIds))
                .ForMember(dest => dest.PreferredPositionsTeamIds, opt => opt.MapFrom(src => src.Body.PreferredPositionsTeamIds))
                .ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.Body.PreferredPartsInner))
                .ForMember(dest => dest.PreferredPartsTeam, opt => opt.MapFrom(src => src.Body.PreferredPartsTeam));

            CreateMap<DoublingInstrumentCreateDto, DoublingInstrumentCreateCommand>();
        }
    }

    public class MusicianProfileCreateDtoValidator : BaseModifyDtoValidator<MusicianProfileCreateDto, MusicianProfileCreateBodyDto>
    {
        public MusicianProfileCreateDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new MusicianProfileCreateBodyDtoValidator());
        }
    }

    public class MusicianProfileCreateBodyDtoValidator : AbstractValidator<MusicianProfileCreateBodyDto>
    {
        public MusicianProfileCreateBodyDtoValidator()
        {
            RuleFor(p => p)
                .NotNull();

            RuleFor(p => p.LevelAssessmentInner)
                .FiveStarRating();
            RuleFor(p => p.LevelAssessmentTeam)
                .FiveStarRating();

            RuleFor(p => p.InstrumentId)
               .NotEmpty();

            RuleFor(p => p.QualificationId)
               .NotEmpty();

            RuleForEach(p => p.DoublingInstruments)
                .SetValidator(new DoublingInstrumentCreateDtoValidator());

            RuleForEach(p => p.PreferredPositionsTeamIds)
                .NotEmpty();

            RuleForEach(p => p.PreferredPositionsInnerIds)
                .NotEmpty();
        }
    }

    public class DoublingInstrumentCreateDtoValidator : AbstractValidator<DoublingInstrumentCreateDto>
    {
        public DoublingInstrumentCreateDtoValidator()
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
