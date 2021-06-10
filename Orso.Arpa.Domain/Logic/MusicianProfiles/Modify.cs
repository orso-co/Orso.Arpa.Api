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

            public byte LevelAssessmentInner { get; set; }
            public byte LevelAssessmentTeam { get; set; }
            public byte ProfilePreferenceInner { get; set; }
            public byte ProfilePreferenceTeam { get; set; }

            public string BackgroundInner { get; set; }
            public string BackgroundTeam { get; set; }
            public string SalaryComment { get; set; }
            #endregion

            #region Reference
            public Guid? QualificationId { get; set; }
            public virtual SelectValueMapping Qualification { get; set; }

            public Guid? SalaryId { get; set; }
            public virtual SelectValueMapping Salary { get; set; }

            public Guid? InquiryStatusInnerId { get; set; }
            public virtual SelectValueMapping InquiryStatusInner { get; set; }

            public Guid? InquiryStatusTeamId { get; set; }
            public virtual SelectValueMapping InquiryStatusTeam { get; set; }
            #endregion

            #region Collection
            public IList<MusicianProfileSection> DoublingInstruments { get; set; } = new List<MusicianProfileSection>();
            public IList<MusicianProfileEducation> MusicianProfileEducations { get; set; } = new List<MusicianProfileEducation>();
            public IList<MusicianProfilePositionInner> PreferredPositionsInner { get; set; } = new List<MusicianProfilePositionInner>();
            //public IList<PreferredPosition> PreferredPositionsTeam { get; set; } = new List<PreferredPosition>();
            //public IList<PreferredPart> PreferredPartsInner { get; set; } = new List<PreferredPart>();
            //public IList<PreferredPart> PreferredPartsTeam { get; set; } = new List<PreferredPart>();

            public IList<MusicianProfileCurriculumVitaeReference> MusicianProfileCurriculumVitaeReferences { get; set; } = new List<MusicianProfileCurriculumVitaeReference>();
            public IList<PreferredGenre> PreferredGenres { get; set; } = new List<PreferredGenre>();
            public IList<AvailableDocument> AvailableDocuments { get; set; } = new List<AvailableDocument>();
            public IList<RegionPreferencePerformance> RegionPreferencePerformances { get; set; } = new List<RegionPreferencePerformance>();
            public IList<RegionPreferenceRehearsal> RegionPreferenceRehearsals { get; set; } = new List<RegionPreferenceRehearsal>();
            public IList<Audition> Auditions { get; set; } = new List<Audition>();
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

                    .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.LevelAssessmentInner))
                    .ForMember(dest => dest.LevelAssessmentTeam, opt => opt.MapFrom(src => src.LevelAssessmentTeam))
                    .ForMember(dest => dest.ProfilePreferenceInner, opt => opt.MapFrom(src => src.ProfilePreferenceInner))
                    .ForMember(dest => dest.ProfilePreferenceTeam, opt => opt.MapFrom(src => src.ProfilePreferenceTeam))

                    .ForMember(dest => dest.BackgroundInner, opt => opt.MapFrom(src => src.BackgroundInner))
                    .ForMember(dest => dest.BackgroundTeam, opt => opt.MapFrom(src => src.BackgroundTeam))
                    .ForMember(dest => dest.SalaryComment, opt => opt.MapFrom(src => src.SalaryComment))

                    .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.QualificationId))
                    .ForMember(dest => dest.SalaryId, opt => opt.MapFrom(src => src.SalaryId))
                    .ForMember(dest => dest.InquiryStatusInnerId, opt => opt.MapFrom(src => src.InquiryStatusInnerId))
                    .ForMember(dest => dest.InquiryStatusTeamId, opt => opt.MapFrom(src => src.InquiryStatusTeamId))

                    .ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.DoublingInstruments))
                    .ForMember(dest => dest.MusicianProfileEducations, opt => opt.MapFrom(src => src.MusicianProfileEducations))
                    //.ForMember(dest => dest.PreferredPositionsInner, opt => opt.MapFrom(src => src.PreferredPositionsInner))
                    //.ForMember(dest => dest.PreferredPositionsTeam, opt => opt.MapFrom(src => src.PreferredPositionsTeam))
                    //.ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.PreferredPartsInner))
                    //.ForMember(dest => dest.PreferredPartsTeam, opt => opt.MapFrom(src => src.PreferredPartsTeam))

                    // TODO da muss Senf rein mit neuen Einträgen hinzufügen, fehlende Löschen u.v.a.m

                    .ForAllOtherMembers(opt => opt.Ignore());
            }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator(IArpaContext arpaContext)
            {
                RuleFor(c => c.Id)
                    .EntityExists<Command, MusicianProfile>(arpaContext, nameof(Command.Id));

                RuleFor(c => c.QualificationId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Qualification);

                RuleFor(c => c.SalaryId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.Salary);

                RuleFor(c => c.InquiryStatusInnerId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusInner);

                RuleFor(c => c.InquiryStatusTeamId)
                    .SelectValueMapping<Command, MusicianProfile>(arpaContext, a => a.InquiryStatusTeam);

                //ToDo Validation for Collections
            }
        }
    }
}
