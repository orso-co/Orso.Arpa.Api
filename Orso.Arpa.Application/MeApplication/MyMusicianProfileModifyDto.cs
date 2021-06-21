using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Logic.Me;

namespace Orso.Arpa.Application.MyMusicianProfileApplication
{
    public class MyMusicianProfileModifyDto : IdFromRouteDto<MyMusicianProfileModifyBodyDto>
    {
    }

    public class MyMusicianProfileModifyBodyDto
    {
        public bool IsMainProfile { get; set; }
        public bool IsDeactivated { get; set; }

        public byte LevelAssessmentInner { get; set; }
        public byte ProfilePreferenceInner { get; set; }
        public string BackgroundInner { get; set; }

        public Guid? InquiryStatusInnerId { get; set; }

        public IList<Guid> PreferredPositionsInnerIds { get; set; } = new List<Guid>();
        public IList<byte> PreferredPartsInner { get; set; } = new List<byte>();
    }

    public class MyMusicianProfileModifyDtoMappingProfile : Profile
    {
        public MyMusicianProfileModifyDtoMappingProfile()
        {
            CreateMap<MyMusicianProfileModifyDto, ModifyMusicianProfile.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.IsMainProfile, opt => opt.MapFrom(src => src.Body.IsMainProfile))
                .ForMember(dest => dest.IsDeactivated, opt => opt.MapFrom(src => src.Body.IsDeactivated))

                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.Body.LevelAssessmentInner))
                .ForMember(dest => dest.ProfilePreferenceInner, opt => opt.MapFrom(src => src.Body.ProfilePreferenceInner))
                .ForMember(dest => dest.BackgroundInner, opt => opt.MapFrom(src => src.Body.BackgroundInner))

                .ForMember(dest => dest.InquiryStatusInnerId, opt => opt.MapFrom(src => src.Body.InquiryStatusInnerId))

                .ForMember(dest => dest.PreferredPositionsInnerIds, opt => opt.MapFrom(src => src.Body.PreferredPositionsInnerIds))
                .ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.Body.PreferredPartsInner))
            ;
        }
    }

    public class MyMusicianProfileModifyDtoValidator : BaseModifyDtoValidator<MyMusicianProfileModifyDto, MyMusicianProfileModifyBodyDto>
    {
        public MyMusicianProfileModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new MyMusicianProfileModifyBodyDtoValidator());

        }
    }

    public class MyMusicianProfileModifyBodyDtoValidator : AbstractValidator<MyMusicianProfileModifyBodyDto>
    {
        public MyMusicianProfileModifyBodyDtoValidator()
        {
            RuleFor(p => p.LevelAssessmentInner)
                .FiveStarRating();
            RuleFor(p => p.ProfilePreferenceInner)
                .FiveStarRating();

            RuleFor(p => p.BackgroundInner)
                .MaximumLength(1000);

            RuleForEach(p => p.PreferredPositionsInnerIds)
                .NotEmpty();
        }
    }
}
