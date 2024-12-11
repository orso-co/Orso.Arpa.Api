using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.DoublingInstrumentApplication.Model;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Enums;

namespace Orso.Arpa.Application.MusicianProfileApplication.Model
{
    public class MusicianProfileCreateDto : IdFromRouteDto<MusicianProfileCreateBodyDto>
    {
    }

    public class MusicianProfileCreateBodyDto
    {
        public byte LevelAssessmentInner { get; set; }
        public byte LevelAssessmentTeam { get; set; }
        public byte ProfilePreferenceInner { get; set; }
        public byte ProfilePreferenceTeam { get; set; }
        public Guid InstrumentId { get; set; }
        public Guid QualificationId { get; set; }
        public MusicianProfileInquiryStatus? InquiryStatusInner { get; set; }
        public MusicianProfileInquiryStatus? InquiryStatusTeam { get; set; }
        public IList<DoublingInstrumentCreateBodyDto> DoublingInstruments { get; set; } = [];
        public IList<Guid> PreferredPositionsInnerIds { get; set; } = [];
        public IList<Guid> PreferredPositionsTeamIds { get; set; } = [];
        public IList<byte> PreferredPartsInner { get; set; } = [];
        public IList<byte> PreferredPartsTeam { get; set; } = [];
    }

    public class MusicianProfileCreateDtoMappingProfile : Profile
    {
        public MusicianProfileCreateDtoMappingProfile()
        {
            _ = CreateMap<MusicianProfileCreateDto, CreateMusicianProfile.Command>()
                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.Body.LevelAssessmentInner))
                .ForMember(dest => dest.LevelAssessmentTeam, opt => opt.MapFrom(src => src.Body.LevelAssessmentTeam))
                .ForMember(dest => dest.ProfilePreferenceInner, opt => opt.MapFrom(src => src.Body.ProfilePreferenceInner))
                .ForMember(dest => dest.ProfilePreferenceTeam, opt => opt.MapFrom(src => src.Body.ProfilePreferenceTeam))
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

            _ = RuleFor(p => p.ProfilePreferenceInner)
                .FiveStarRating();

            _ = RuleFor(p => p.ProfilePreferenceTeam)
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
