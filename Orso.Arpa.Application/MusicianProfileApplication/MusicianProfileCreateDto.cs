using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.MusicianProfiles.Create;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MusicianProfileCreateDto
    {
        #region Native
        public byte LevelAssessmentPerformer { get; set; }
        public byte LevelAssessmentStaff { get; set; }
        public byte ProfilePreferencePerformer { get; set; }
        public byte ProfilePreferenceStaff { get; set; }
        public bool IsMainProfile { get; set; }
        public string Background { get; set; }
        public byte ExperienceLevel { get; set; }
        public string SalaryComment { get; set; }
        #endregion

        #region Reference
        public Guid PersonId { get; set; }
        public Guid InstrumentId { get; set; }
        public Guid QualificationId { get; set; }
        public Guid? SalaryId { get; set; }
        public Guid? InquiryStatusPerformerId { get; set; }
        public Guid? InquiryStatusStaffId { get; set; }
        #endregion
    }

    public class MusicianProfileCreateDtoMappingProfile : Profile
    {
        public MusicianProfileCreateDtoMappingProfile()
        {
            CreateMap<MusicianProfileCreateDto, Command>()
                .ForMember(dest => dest.LevelAssessmentPerformer, opt => opt.MapFrom(src => src.LevelAssessmentPerformer))
                .ForMember(dest => dest.ProfilePreferencePerformer, opt => opt.MapFrom(src => src.ProfilePreferencePerformer))
                .ForMember(dest => dest.ProfilePreferenceStaff, opt => opt.MapFrom(src => src.ProfilePreferenceStaff))
                .ForMember(dest => dest.IsMainProfile, opt => opt.MapFrom(src => src.IsMainProfile))
                .ForMember(dest => dest.Background, opt => opt.MapFrom(src => src.Background))
                .ForMember(dest => dest.ExperienceLevel, opt => opt.MapFrom(src => src.ExperienceLevel))
                .ForMember(dest => dest.SalaryComment, opt => opt.MapFrom(src => src.SalaryComment))

                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.InstrumentId))
                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.QualificationId))
                .ForMember(dest => dest.SalaryId, opt => opt.MapFrom(src => src.SalaryId))
                .ForMember(dest => dest.InquiryStatusPerformerId, opt => opt.MapFrom(src => src.InquiryStatusPerformerId))
                .ForMember(dest => dest.InquiryStatusStaffId, opt => opt.MapFrom(src => src.InquiryStatusStaffId));
        }
    }
    public class MusicianProfileCreateDtoValidator : AbstractValidator<MusicianProfileCreateDto>
    {
        public MusicianProfileCreateDtoValidator()
        {
            RuleFor(p => p)
                .NotNull();

            RuleFor(p => p.LevelAssessmentPerformer)
                .InclusiveBetween<MusicianProfileCreateDto, byte>(1, 5);
            RuleFor(p => p.ProfilePreferencePerformer)
                .InclusiveBetween<MusicianProfileCreateDto, byte>(1, 5);
            RuleFor(p => p.ProfilePreferenceStaff)
                .InclusiveBetween<MusicianProfileCreateDto, byte>(1, 5);
            RuleFor(p => p.Background)
                .MaximumLength(1000);
            RuleFor(p => p.ExperienceLevel)
                .InclusiveBetween<MusicianProfileCreateDto, byte>(1, 5);
            RuleFor(p => p.SalaryComment)
                .MaximumLength(500);

            RuleFor(p => p.PersonId)
               .NotEmpty();
            RuleFor(p => p.InstrumentId)
               .NotEmpty();
        }
    }

}
