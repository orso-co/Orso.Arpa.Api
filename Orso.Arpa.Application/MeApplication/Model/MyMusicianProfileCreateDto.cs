using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.MusicianProfileDomain.Commands;
using Orso.Arpa.Domain.MusicianProfileDomain.Enums;

namespace Orso.Arpa.Application.MeApplication.Model
{
    public class MyMusicianProfileCreateDto
    {
        public byte LevelAssessmentInner { get; set; }
        public byte ProfilePreferenceInner { get; set; }
        public Guid InstrumentId { get; set; }
        public MusicianProfileInquiryStatus? InquiryStatusInner { get; set; }
        public IList<MyDoublingInstrumentCreateBodyDto> DoublingInstruments { get; set; } = [];
        public IList<Guid> PreferredPositionsInnerIds { get; set; } = [];
        public IList<byte> PreferredPartsInner { get; set; } = [];
        public string BackgroundInner { get; set; }
    }

    public class MyMusicianProfileCreateDtoMappingProfile : Profile
    {
        public MyMusicianProfileCreateDtoMappingProfile()
        {
            _ = CreateMap<MyMusicianProfileCreateDto, CreateMusicianProfile.Command>()
                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.LevelAssessmentInner))
                .ForMember(dest => dest.ProfilePreferenceInner, opt => opt.MapFrom(src => src.ProfilePreferenceInner))
                .ForMember(dest => dest.BackgroundInner, opt => opt.MapFrom(src => src.BackgroundInner))

                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.InstrumentId))
                .ForMember(dest => dest.InquiryStatusInner, opt => opt.MapFrom(src => src.InquiryStatusInner))

                .ForMember(dest => dest.PreferredPositionsInnerIds, opt => opt.MapFrom(src => src.PreferredPositionsInnerIds))
                .ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.PreferredPartsInner));
        }
    }

    public class MyMusicianProfileCreateDtoValidator : AbstractValidator<MyMusicianProfileCreateDto>
    {
        public MyMusicianProfileCreateDtoValidator()
        {
            _ = RuleFor(p => p)
                .NotNull();

            _ = RuleFor(p => p.LevelAssessmentInner)
                .FiveStarRating()
                .NotEqual((byte)0);

            _ = RuleFor(p => p.ProfilePreferenceInner)
                .FiveStarRating();

            _ = RuleFor(p => p.InstrumentId)
               .NotEmpty();

            _ = RuleFor(p => p.DoublingInstruments)
                .NotNull();

            _ = RuleFor(p => p.PreferredPositionsInnerIds)
                .NotNull();

            _ = RuleFor(p => p.PreferredPartsInner)
                .NotNull();

            _ = RuleForEach(p => p.DoublingInstruments)
                .SetValidator(new MyDoublingInstrumentCreateBodyDtoValidator());

            _ = RuleForEach(p => p.PreferredPositionsInnerIds)
              .NotEmpty();

            _ = RuleFor(p => p.InquiryStatusInner)
                .IsInEnum();

            _ = RuleFor(p => p.BackgroundInner)
                .RestrictedFreeText(1000);
        }
    }
}
