using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Interfaces;
using Orso.Arpa.Domain.Entities;
using static Orso.Arpa.Domain.Logic.MusicianProfiles.Modify;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MusicianProfileModifyDto : IModifyDto
    {
        public Guid Id { get; set; }

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
        public Guid PersonId { get; set; }

        public Guid InstrumentId { get; set; }

        public Guid QualificationId { get; set; }

        public Guid? SalaryId { get; set; }

        public Guid? InquiryStatusPerformerId { get; set; }

        public Guid? InquiryStatusStaffId { get; set; }
        #endregion

        #region Collection
        public virtual ICollection<MusicianProfileSection> DoublingInstruments { get; set; } = new HashSet<MusicianProfileSection>();
        public virtual ICollection<MusicianProfileEducation> MusicianProfileEducations { get; set; } = new HashSet<MusicianProfileEducation>();
        public virtual ICollection<PreferredPosition> PreferredPositionsPerformer { get; set; } = new HashSet<PreferredPosition>();
        //public virtual ICollection<PreferredPosition> PreferredPositionsStaff { get; set; } = new HashSet<PreferredPosition>();
        public virtual ICollection<PreferredPart> PreferredPartsPerformer { get; set; } = new HashSet<PreferredPart>();
        //public virtual ICollection<PreferredPart> PreferredPartsStaff { get; set; } = new HashSet<PreferredPart>();

        #endregion
    }

    public class MusicianProfileModifyDtoMappingProfile : Profile
    {
        public MusicianProfileModifyDtoMappingProfile()
        {
            CreateMap<MusicianProfileModifyDto, Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))

                .ForMember(dest => dest.IsMainProfile, opt => opt.MapFrom(src => src.IsMainProfile))
                .ForMember(dest => dest.IsDeactivated, opt => opt.MapFrom(src => src.IsDeactivated))

                .ForMember(dest => dest.LevelAssessmentPerformer, opt => opt.MapFrom(src => src.LevelAssessmentPerformer))
                .ForMember(dest => dest.LevelAssessmentStaff, opt => opt.MapFrom(src => src.LevelAssessmentStaff))
                .ForMember(dest => dest.ProfilePreferencePerformer, opt => opt.MapFrom(src => src.ProfilePreferencePerformer))
                .ForMember(dest => dest.ProfilePreferenceStaff, opt => opt.MapFrom(src => src.ProfilePreferenceStaff))
                .ForMember(dest => dest.BackgroundPerformer, opt => opt.MapFrom(src => src.BackgroundPerformer))
                .ForMember(dest => dest.BackgroundStaff, opt => opt.MapFrom(src => src.BackgroundStaff))
                .ForMember(dest => dest.SalaryComment, opt => opt.MapFrom(src => src.SalaryComment))

                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.InstrumentId))
                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.QualificationId))
                .ForMember(dest => dest.SalaryId, opt => opt.MapFrom(src => src.SalaryId))
                .ForMember(dest => dest.InquiryStatusPerformerId, opt => opt.MapFrom(src => src.InquiryStatusPerformerId))
                .ForMember(dest => dest.InquiryStatusStaffId, opt => opt.MapFrom(src => src.InquiryStatusStaffId))

                .ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.DoublingInstruments))
                .ForMember(dest => dest.MusicianProfileEducations, opt => opt.MapFrom(src => src.MusicianProfileEducations))
                //.ForMember(dest => dest.PreferredPositionsPerformer, opt => opt.MapFrom(src => src.PreferredPositionsPerformer))
                //.ForMember(dest => dest.PreferredPositionsStaff, opt => opt.MapFrom(src => src.PreferredPositionsStaff))
                //.ForMember(dest => dest.PreferredPartsPerformer, opt => opt.MapFrom(src => src.PreferredPartsPerformer))
                //.ForMember(dest => dest.PreferredPartsStaff, opt => opt.MapFrom(src => src.PreferredPartsStaff))
                ;
        }
    }

    public class MusicianProfileModifyDtoValidator : AbstractValidator<MusicianProfileModifyDto>
    {
        public MusicianProfileModifyDtoValidator()
        {
            RuleFor(p => p)
                .NotNull();
            RuleFor(p => p.Id)
                .NotEmpty();

            RuleFor(p => p.LevelAssessmentPerformer)
                .InclusiveBetween<MusicianProfileModifyDto, byte>(0, 5);
            RuleFor(p => p.LevelAssessmentStaff)
                .InclusiveBetween<MusicianProfileModifyDto, byte>(0, 5);
            RuleFor(p => p.ProfilePreferencePerformer)
                .InclusiveBetween<MusicianProfileModifyDto, byte>(0, 5);
            RuleFor(p => p.ProfilePreferenceStaff)
                .InclusiveBetween<MusicianProfileModifyDto, byte>(0, 5);

            RuleFor(p => p.BackgroundPerformer)
                .MaximumLength(1000);
            RuleFor(p => p.BackgroundStaff)
                .MaximumLength(1000);
            RuleFor(p => p.SalaryComment)
                .MaximumLength(500);

            RuleFor(p => p.PersonId)
               .NotEmpty();
            RuleFor(p => p.InstrumentId)
               .NotEmpty();
            RuleFor(p => p.QualificationId)
               .NotEmpty();

            //ToDo Validation for Collections
        }
    }

}
