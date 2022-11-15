using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.DoublingInstrumentApplication;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

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
        public MusicianProfileInquiryStatus? InquiryStatusInner { get; set; }
        public MusicianProfileInquiryStatus? InquiryStatusTeam { get; set; }
        public IList<DoublingInstrumentCreateBodyDto> DoublingInstruments { get; set; } = new List<DoublingInstrumentCreateBodyDto>();
        public IList<Guid> PreferredPositionsInnerIds { get; set; } = new List<Guid>();
        public IList<Guid> PreferredPositionsTeamIds { get; set; } = new List<Guid>();
        public IList<byte> PreferredPartsInner { get; set; } = new List<byte>();
        public IList<byte> PreferredPartsTeam { get; set; } = new List<byte>();
    }

    public class MusicianProfileCreateDtoMappingProfile : Profile
    {
        public MusicianProfileCreateDtoMappingProfile()
        {
            _ = CreateMap<MusicianProfileCreateDto, Create.Command>()
                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.Body.LevelAssessmentInner))
                .ForMember(dest => dest.LevelAssessmentTeam, opt => opt.MapFrom(src => src.Body.LevelAssessmentTeam))

                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.Body.InstrumentId))
                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.Body.QualificationId))
                .ForMember(dest => dest.InquiryStatusInner, opt => opt.MapFrom(src => src.Body.InquiryStatusInner))
                .ForMember(dest => dest.InquiryStatusTeam, opt => opt.MapFrom(src => src.Body.InquiryStatusTeam))

                .ForMember(dest => dest.PreferredPositionsInnerIds, opt => opt.MapFrom(src => src.Body.PreferredPositionsInnerIds))
                .ForMember(dest => dest.PreferredPositionsTeamIds, opt => opt.MapFrom(src => src.Body.PreferredPositionsTeamIds))
                .ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.Body.PreferredPartsInner))
                .ForMember(dest => dest.PreferredPartsTeam, opt => opt.MapFrom(src => src.Body.PreferredPartsTeam));
        }
    }

    public class MusicianProfileCreateDtoValidator : IdFromRouteDtoValidator<MusicianProfileCreateDto, MusicianProfileCreateBodyDto>
    {
        public MusicianProfileCreateDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                .SetValidator(new MusicianProfileCreateBodyDtoValidator());
        }
    }

    public class MusicianProfileCreateBodyDtoValidator : AbstractValidator<MusicianProfileCreateBodyDto>
    {
        public MusicianProfileCreateBodyDtoValidator()
        {
            _ = RuleFor(p => p)
                .NotNull();

            _ = RuleFor(p => p.DoublingInstruments)
                .NotNull();

            _ = RuleFor(p => p.PreferredPositionsInnerIds)
                .NotNull();

            _ = RuleFor(p => p.PreferredPositionsTeamIds)
                .NotNull();

            _ = RuleFor(p => p.PreferredPartsInner)
                .NotNull();

            _ = RuleFor(p => p.PreferredPartsTeam)
                .NotNull();

            _ = RuleFor(p => p.LevelAssessmentInner)
                .FiveStarRating();
            _ = RuleFor(p => p.LevelAssessmentTeam)
                .FiveStarRating();

            _ = RuleFor(p => p.InstrumentId)
               .NotEmpty();

            _ = RuleFor(p => p.QualificationId)
               .NotEmpty();

            _ = RuleForEach(p => p.DoublingInstruments)
                .SetValidator(new DoublingInstrumentCreateBodyDtoValidator());

            _ = RuleForEach(p => p.PreferredPositionsTeamIds)
                .NotEmpty();

            _ = RuleForEach(p => p.PreferredPositionsInnerIds)
                .NotEmpty();

            _ = RuleFor(p => p.InquiryStatusInner)
                .IsInEnum();

            _ = RuleFor(p => p.InquiryStatusTeam)
                .IsInEnum();
        }
    }
}
