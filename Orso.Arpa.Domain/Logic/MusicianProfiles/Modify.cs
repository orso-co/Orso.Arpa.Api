using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Entities;
using Orso.Arpa.Domain.Extensions;
using Orso.Arpa.Domain.Interfaces;
using static Orso.Arpa.Domain.GenericHandlers.Modify;

namespace Orso.Arpa.Domain.Logic.MusicianProfiles
{
    public static class Modify
    {
        public class Command : IModifyCommand<MusicianProfile>
        {
            public Guid Id { get; set; }

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
            public Guid? QualificationId { get; set; }
            public Guid? SalaryId { get; set; }
            public Guid? InquiryStatusPerformerId { get; set; }
            public Guid? InquiryStatusStaffId { get; set; }
            #endregion
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, MusicianProfile>()
                    .ForMember(dest => dest.LevelAssessmentPerformer, opt => opt.MapFrom(src => src.LevelAssessmentPerformer))
                    .ForMember(dest => dest.LevelAssessmentStaff, opt => opt.MapFrom(src => src.LevelAssessmentStaff))
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
                    .ForMember(dest => dest.InquiryStatusStaffId, opt => opt.MapFrom(src => src.InquiryStatusStaffId))
                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, MusicianProfile>(arpaContext, nameof(Command.Id));

                RuleFor(c => c.PersonId)
                     .EntityExists<Command, Person>(arpaContext, nameof(Command.PersonId));

                RuleFor(c => c.InstrumentId)
                    .EntityExists<Command, Section>(arpaContext, nameof(Command.InstrumentId));

                RuleFor(c => c.QualificationId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Qualification);

                RuleFor(c => c.SalaryId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Salary);

                RuleFor(c => c.InquiryStatusPerformerId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusPerformer);

                RuleFor(c => c.InquiryStatusStaffId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusStaff);

            }
        }
    }
}
