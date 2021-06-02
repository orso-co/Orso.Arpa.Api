using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.MusicianProfiles.Modify;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MusicianProfileModifyDto : IdFromRouteDto<MusicianProfileModifyBodyDto>
    {
    }

    public class MusicianProfileModifyBodyDto
    {
        #region Native
        public bool IsMainProfile { get; set; }
        public bool IsDeactivated { get; set; }

        public byte LevelAssessmentInner { get; set; }
        public byte LevelAssessmentTeam { get; set; }
        public byte ProfilePreferenceInner { get; set; }
        public byte ProfilePreferenceTeam { get; set; }

        public string BackgroundInner { get; set; }
        public string BackgroundTeam { get; set; }
        public string SalaryComment { get; set; }
        #endregion

        #region Reference
        public Guid QualificationId { get; set; }

        public Guid? SalaryId { get; set; }

        public Guid? InquiryStatusInnerId { get; set; }

        public Guid? InquiryStatusTeamId { get; set; }
        #endregion

        #region Collection
        //public IList<MusicianProfileSection> DoublingInstruments { get; set; } = new List<MusicianProfileSection>();
        //public IList<MusicianProfileEducation> MusicianProfileEducations { get; set; } = new List<MusicianProfileEducation>();
        //public IList<PreferredPosition> PreferredPositionsInner { get; set; } = new List<PreferredPosition>();
        //public List<PreferredPosition> PreferredPositionsTeam { get; set; } = new List<PreferredPosition>();
        //public IList<PreferredPart> PreferredPartsInner { get; set; } = new List<PreferredPart>();
        //public IList<PreferredPart> PreferredPartsTeam { get; set; } = new List<PreferredPart>();

        #endregion
    }

    public class MusicianProfileModifyDtoMappingProfile : Profile
    {
        public MusicianProfileModifyDtoMappingProfile()
        {
            CreateMap<MusicianProfileModifyDto, Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.IsMainProfile, opt => opt.MapFrom(src => src.Body.IsMainProfile))
                .ForMember(dest => dest.IsDeactivated, opt => opt.MapFrom(src => src.Body.IsDeactivated))

                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.Body.LevelAssessmentInner))
                .ForMember(dest => dest.LevelAssessmentTeam, opt => opt.MapFrom(src => src.Body.LevelAssessmentTeam))
                .ForMember(dest => dest.ProfilePreferenceInner, opt => opt.MapFrom(src => src.Body.ProfilePreferenceInner))
                .ForMember(dest => dest.ProfilePreferenceTeam, opt => opt.MapFrom(src => src.Body.ProfilePreferenceTeam))
                .ForMember(dest => dest.BackgroundInner, opt => opt.MapFrom(src => src.Body.BackgroundInner))
                .ForMember(dest => dest.BackgroundTeam, opt => opt.MapFrom(src => src.Body.BackgroundTeam))
                .ForMember(dest => dest.SalaryComment, opt => opt.MapFrom(src => src.Body.SalaryComment))

                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.Body.QualificationId))
                .ForMember(dest => dest.SalaryId, opt => opt.MapFrom(src => src.Body.SalaryId))
                .ForMember(dest => dest.InquiryStatusInnerId, opt => opt.MapFrom(src => src.Body.InquiryStatusInnerId))
                .ForMember(dest => dest.InquiryStatusTeamId, opt => opt.MapFrom(src => src.Body.InquiryStatusTeamId))

                //.ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.Body.DoublingInstruments))
                //.ForMember(dest => dest.MusicianProfileEducations, opt => opt.MapFrom(src => src.Body.MusicianProfileEducations))
                //.ForMember(dest => dest.PreferredPositionsInner, opt => opt.MapFrom(src => src.Body.PreferredPositionsInner))
                //.ForMember(dest => dest.PreferredPositionsTeam, opt => opt.MapFrom(src => src.Body.PreferredPositionsTeam))
                //.ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.Body.PreferredPartsInner))
                //.ForMember(dest => dest.PreferredPartsTeam, opt => opt.MapFrom(src => src.Body.PreferredPartsTeam))
                ;
        }
    }

    public class MusicianProfileModifyDtoValidator : BaseModifyDtoValidator<MusicianProfileModifyDto, MusicianProfileModifyBodyDto>
    {
        public MusicianProfileModifyDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new MusicianProfileModifyBodyDtoValidator());
        }
    }

    public class MusicianProfileModifyBodyDtoValidator : AbstractValidator<MusicianProfileModifyBodyDto>
    {
        public MusicianProfileModifyBodyDtoValidator()
        {
            RuleFor(p => p.LevelAssessmentInner)
                .FiveStarRating();
            RuleFor(p => p.LevelAssessmentTeam)
                .FiveStarRating();
            RuleFor(p => p.ProfilePreferenceInner)
                .FiveStarRating();
            RuleFor(p => p.ProfilePreferenceTeam)
                .FiveStarRating();

            RuleFor(p => p.BackgroundInner)
                .MaximumLength(1000);
            RuleFor(p => p.BackgroundTeam)
                .MaximumLength(1000);
            RuleFor(p => p.SalaryComment)
                .MaximumLength(500);

            RuleFor(p => p.QualificationId)
               .NotEmpty();

            //ToDo Validation for Collections
        }
    }
}
