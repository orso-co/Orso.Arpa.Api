using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Application.MeApplication
{
    public class MyMusicianProfileModifyDto : IdFromRouteDto<MyMusicianProfileModifyBodyDto>
    {
    }

    public class MyMusicianProfileModifyBodyDto
    {
        public bool IsMainProfile { get; set; }

        public byte LevelAssessmentInner { get; set; }
        public byte ProfilePreferenceInner { get; set; }
        public string BackgroundInner { get; set; }

        public MusicianProfileInquiryStatus? InquiryStatusInner { get; set; }

        public IList<Guid> PreferredPositionsInnerIds { get; set; } = new List<Guid>();
        public IList<byte> PreferredPartsInner { get; set; } = new List<byte>();
    }

    public class MyMusicianProfileModifyDtoMappingProfile : Profile
    {
        public MyMusicianProfileModifyDtoMappingProfile()
        {
            _ = CreateMap<MyMusicianProfileModifyDto, ModifyMusicianProfile.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.IsMainProfile, opt => opt.MapFrom(src => src.Body.IsMainProfile))

                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.Body.LevelAssessmentInner))
                .ForMember(dest => dest.ProfilePreferenceInner, opt => opt.MapFrom(src => src.Body.ProfilePreferenceInner))
                .ForMember(dest => dest.BackgroundInner, opt => opt.MapFrom(src => src.Body.BackgroundInner))

                .ForMember(dest => dest.InquiryStatusInner, opt => opt.MapFrom(src => src.Body.InquiryStatusInner))

                .ForMember(dest => dest.PreferredPositionsInnerIds, opt => opt.MapFrom(src => src.Body.PreferredPositionsInnerIds))
                .ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.Body.PreferredPartsInner));
        }
    }

    public class MyMusicianProfileModifyDtoValidator : IdFromRouteDtoValidator<MyMusicianProfileModifyDto, MyMusicianProfileModifyBodyDto>
    {
        public MyMusicianProfileModifyDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                .SetValidator(new MyMusicianProfileModifyBodyDtoValidator());

        }
    }

    public class MyMusicianProfileModifyBodyDtoValidator : AbstractValidator<MyMusicianProfileModifyBodyDto>
    {
        public MyMusicianProfileModifyBodyDtoValidator()
        {
            _ = RuleFor(p => p.LevelAssessmentInner)
                .FiveStarRating();
            _ = RuleFor(p => p.ProfilePreferenceInner)
                .FiveStarRating();

            _ = RuleFor(p => p.BackgroundInner)
                .RestrictedFreeText(1000);

            _ = RuleForEach(p => p.PreferredPositionsInnerIds)
                .NotEmpty();

            _ = RuleFor(p => p.PreferredPartsInner)
                .NotNull();

            _ = RuleFor(p => p.PreferredPositionsInnerIds)
                .NotNull();

            _ = RuleFor(p => p.InquiryStatusInner)
                .IsInEnum();
        }
    }
}
