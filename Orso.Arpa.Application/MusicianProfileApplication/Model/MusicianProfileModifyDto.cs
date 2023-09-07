using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Enums;
using Orso.Arpa.Domain.Logic.MusicianProfiles;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MusicianProfileModifyDto : IdFromRouteDto<MusicianProfileModifyBodyDto>
    {
    }

    public class MusicianProfileModifyBodyDto
    {
        public bool IsMainProfile { get; set; }
        public byte LevelAssessmentInner { get; set; }
        public byte LevelAssessmentTeam { get; set; }
        public byte ProfilePreferenceInner { get; set; }
        public byte ProfilePreferenceTeam { get; set; }

        public string BackgroundInner { get; set; }
        public string BackgroundTeam { get; set; }
        public string SalaryComment { get; set; }
        public Guid QualificationId { get; set; }

        public Guid? SalaryId { get; set; }

        public MusicianProfileInquiryStatus? InquiryStatusInner { get; set; }

        public MusicianProfileInquiryStatus? InquiryStatusTeam { get; set; }

        public IList<Guid> PreferredPositionsInnerIds { get; set; } = new List<Guid>();
        public IList<Guid> PreferredPositionsTeamIds { get; set; } = new List<Guid>();
        public IList<byte> PreferredPartsInner { get; set; } = new List<byte>();
        public IList<byte> PreferredPartsTeam { get; set; } = new List<byte>();
    }

    public class MusicianProfileModifyDtoMappingProfile : Profile
    {
        public MusicianProfileModifyDtoMappingProfile()
        {
            _ = CreateMap<MusicianProfileModifyDto, Modify.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.IsMainProfile, opt => opt.MapFrom(src => src.Body.IsMainProfile))

                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.Body.LevelAssessmentInner))
                .ForMember(dest => dest.LevelAssessmentTeam, opt => opt.MapFrom(src => src.Body.LevelAssessmentTeam))
                .ForMember(dest => dest.ProfilePreferenceInner, opt => opt.MapFrom(src => src.Body.ProfilePreferenceInner))
                .ForMember(dest => dest.ProfilePreferenceTeam, opt => opt.MapFrom(src => src.Body.ProfilePreferenceTeam))
                .ForMember(dest => dest.BackgroundInner, opt => opt.MapFrom(src => src.Body.BackgroundInner))
                .ForMember(dest => dest.BackgroundTeam, opt => opt.MapFrom(src => src.Body.BackgroundTeam))
                .ForMember(dest => dest.SalaryComment, opt => opt.MapFrom(src => src.Body.SalaryComment))

                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.Body.QualificationId))
                .ForMember(dest => dest.SalaryId, opt => opt.MapFrom(src => src.Body.SalaryId))
                .ForMember(dest => dest.InquiryStatusInner, opt => opt.MapFrom(src => src.Body.InquiryStatusInner))
                .ForMember(dest => dest.InquiryStatusTeam, opt => opt.MapFrom(src => src.Body.InquiryStatusTeam))

                .ForMember(dest => dest.PreferredPositionsInnerIds, opt => opt.MapFrom(src => src.Body.PreferredPositionsInnerIds))
                .ForMember(dest => dest.PreferredPositionsTeamIds, opt => opt.MapFrom(src => src.Body.PreferredPositionsTeamIds))
                .ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.Body.PreferredPartsInner))
                .ForMember(dest => dest.PreferredPartsTeam, opt => opt.MapFrom(src => src.Body.PreferredPartsTeam));
        }
    }

    public class MusicianProfileModifyDtoValidator : IdFromRouteDtoValidator<MusicianProfileModifyDto, MusicianProfileModifyBodyDto>
    {
        public MusicianProfileModifyDtoValidator()
        {
            _ = RuleFor(d => d.Body)
                .SetValidator(new MusicianProfileModifyBodyDtoValidator());
        }
    }

    public class MusicianProfileModifyBodyDtoValidator : AbstractValidator<MusicianProfileModifyBodyDto>
    {
        public MusicianProfileModifyBodyDtoValidator()
        {
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

            _ = RuleFor(p => p.BackgroundInner)
                .RestrictedFreeText(1000);

            _ = RuleFor(p => p.BackgroundTeam)
                .RestrictedFreeText(1000);

            _ = RuleFor(p => p.SalaryComment)
                .RestrictedFreeText(500);

            _ = RuleFor(p => p.QualificationId)
               .NotEmpty();

            RuleFor(p => p.InquiryStatusInner)
                .IsInEnum();

            RuleFor(p => p.InquiryStatusTeam)
                .IsInEnum();

            _ = RuleForEach(p => p.PreferredPositionsTeamIds)
                .NotEmpty();

            _ = RuleForEach(p => p.PreferredPositionsInnerIds)
                .NotEmpty();
        }
    }
}
