using System;
using System.Collections.Generic;
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
            public virtual Person Person { get; set; }

            public Guid InstrumentId { get; set; }
            public virtual Section Instrument { get; set; }

            public Guid? QualificationId { get; set; }
            public virtual SelectValueMapping Qualification { get; set; }

            public Guid? SalaryId { get; set; }
            public virtual SelectValueMapping Salary { get; set; }

            public Guid? InquiryStatusPerformerId { get; set; }
            public virtual SelectValueMapping InquiryStatusPerformer { get; set; }

            public Guid? InquiryStatusStaffId { get; set; }
            public virtual SelectValueMapping InquiryStatusStaff { get; set; }
            #endregion

            #region Collection
            public virtual ICollection<MusicianProfileSection> DoublingInstruments { get; set; } = new HashSet<MusicianProfileSection>();
            public virtual ICollection<MusicianProfileEducation> MusicianProfileEducations { get; set; } = new HashSet<MusicianProfileEducation>();
            public virtual ICollection<PreferredPosition> PreferredPositionsPerformer { get; set; } = new HashSet<PreferredPosition>();
            //public virtual ICollection<PreferredPosition> PreferredPositionsStaff { get; set; } = new HashSet<PreferredPosition>();
            public virtual ICollection<PreferredPart> PreferredPartsPerformer { get; set; } = new HashSet<PreferredPart>();
            //public virtual ICollection<PreferredPart> PreferredPartsStaff { get; set; } = new HashSet<PreferredPart>();

            public virtual ICollection<MusicianProfileCurriculumVitaeReference> MusicianProfileCurriculumVitaeReferences { get; set; } = new HashSet<MusicianProfileCurriculumVitaeReference>();
            public virtual ICollection<PreferredGenre> PreferredGenres { get; set; } = new HashSet<PreferredGenre>();
            public virtual ICollection<AvailableDocument> AvailableDocuments { get; set; } = new HashSet<AvailableDocument>();
            public virtual ICollection<RegionPreferencePerformance> RegionPreferencePerformances { get; set; } = new HashSet<RegionPreferencePerformance>();
            public virtual ICollection<RegionPreferenceRehearsal> RegionPreferenceRehearsals { get; set; } = new HashSet<RegionPreferenceRehearsal>();
            public virtual ICollection<Audition> Auditions { get; set; } = new HashSet<Audition>();
            #endregion
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Command, MusicianProfile>()
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

                //ToDo Validation for Collections
            }
        }
    }
}
