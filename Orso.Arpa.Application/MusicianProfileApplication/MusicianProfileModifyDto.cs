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

        public byte LevelAssessmentPerformer { get; set; }
        public byte LevelAssessmentStaff { get; set; }
        public byte ProfilePreferencePerformer { get; set; }
        public byte ProfilePreferenceStaff { get; set; }

        public string BackgroundPerformer { get; set; }
        public string BackgroundStaff { get; set; }
        public string SalaryComment { get; set; }
        #endregion

        #region Reference
        public Guid QualificationId { get; set; }

        public Guid? SalaryId { get; set; }

        public Guid? InquiryStatusPerformerId { get; set; }

        public Guid? InquiryStatusStaffId { get; set; }
        #endregion

        #region Collection
        //public IList<MusicianProfileSection> DoublingInstruments { get; set; } = new List<MusicianProfileSection>();
        //public IList<MusicianProfileEducation> MusicianProfileEducations { get; set; } = new List<MusicianProfileEducation>();
        //public IList<PreferredPosition> PreferredPositionsPerformer { get; set; } = new List<PreferredPosition>();
        //public List<PreferredPosition> PreferredPositionsStaff { get; set; } = new List<PreferredPosition>();
        //public IList<PreferredPart> PreferredPartsPerformer { get; set; } = new List<PreferredPart>();
        //public IList<PreferredPart> PreferredPartsStaff { get; set; } = new List<PreferredPart>();

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

                .ForMember(dest => dest.LevelAssessmentPerformer, opt => opt.MapFrom(src => src.Body.LevelAssessmentPerformer))
                .ForMember(dest => dest.LevelAssessmentStaff, opt => opt.MapFrom(src => src.Body.LevelAssessmentStaff))
                .ForMember(dest => dest.ProfilePreferencePerformer, opt => opt.MapFrom(src => src.Body.ProfilePreferencePerformer))
                .ForMember(dest => dest.ProfilePreferenceStaff, opt => opt.MapFrom(src => src.Body.ProfilePreferenceStaff))
                .ForMember(dest => dest.BackgroundPerformer, opt => opt.MapFrom(src => src.Body.BackgroundPerformer))
                .ForMember(dest => dest.BackgroundStaff, opt => opt.MapFrom(src => src.Body.BackgroundStaff))
                .ForMember(dest => dest.SalaryComment, opt => opt.MapFrom(src => src.Body.SalaryComment))

                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.Body.QualificationId))
                .ForMember(dest => dest.SalaryId, opt => opt.MapFrom(src => src.Body.SalaryId))
                .ForMember(dest => dest.InquiryStatusPerformerId, opt => opt.MapFrom(src => src.Body.InquiryStatusPerformerId))
                .ForMember(dest => dest.InquiryStatusStaffId, opt => opt.MapFrom(src => src.Body.InquiryStatusStaffId))

                //.ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.Body.DoublingInstruments))
                //.ForMember(dest => dest.MusicianProfileEducations, opt => opt.MapFrom(src => src.Body.MusicianProfileEducations))
                //.ForMember(dest => dest.PreferredPositionsPerformer, opt => opt.MapFrom(src => src.Body.PreferredPositionsPerformer))
                //.ForMember(dest => dest.PreferredPositionsStaff, opt => opt.MapFrom(src => src.Body.PreferredPositionsStaff))
                //.ForMember(dest => dest.PreferredPartsPerformer, opt => opt.MapFrom(src => src.Body.PreferredPartsPerformer))
                //.ForMember(dest => dest.PreferredPartsStaff, opt => opt.MapFrom(src => src.Body.PreferredPartsStaff))
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
            RuleFor(p => p.LevelAssessmentPerformer)
                .FiveStarRating();
            RuleFor(p => p.LevelAssessmentStaff)
                .FiveStarRating();
            RuleFor(p => p.ProfilePreferencePerformer)
                .FiveStarRating();
            RuleFor(p => p.ProfilePreferenceStaff)
                .FiveStarRating();

            RuleFor(p => p.BackgroundPerformer)
                .MaximumLength(1000);
            RuleFor(p => p.BackgroundStaff)
                .MaximumLength(1000);
            RuleFor(p => p.SalaryComment)
                .MaximumLength(500);

            RuleFor(p => p.QualificationId)
               .NotEmpty();

            //ToDo Validation for Collections
        }
    }
}
