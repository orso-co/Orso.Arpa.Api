using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication.Model;
using Orso.Arpa.Application.DoublingInstrumentApplication.Model;
using Orso.Arpa.Application.EducationApplication.Model;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication.Model;
using Orso.Arpa.Application.SectionApplication.Model;
using Orso.Arpa.Application.SelectValueApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Enums;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;
using Orso.Arpa.Domain.RegionDomain.Enums;

namespace Orso.Arpa.Application.MusicianProfileApplication.Model
{
    public class MusicianProfileDto : BaseEntityDto
    {
        public bool IsMainProfile { get; set; }
        public byte LevelAssessmentInner { get; set; }
        public byte LevelAssessmentTeam { get; set; }
        public byte ProfilePreferenceInner { get; set; }
        public byte ProfilePreferenceTeam { get; set; }

        public string BackgroundInner { get; set; }
        public string BackgroundTeam { get; set; }
        public string SalaryComment { get; set; }
        public Guid PersonId { get; set; }
        public SectionDto Instrument { get; set; }
        public Guid? QualificationId { get; set; }
        public Guid? SalaryId { get; set; }
        public MusicianProfileInquiryStatus? InquiryStatusInner { get; set; }
        public MusicianProfileInquiryStatus? InquiryStatusTeam { get; set; }
        public IList<DoublingInstrumentDto> DoublingInstruments { get; set; } = [];
        public IList<EducationDto> Educations { get; set; } = [];
        public IList<CurriculumVitaeReferenceDto> CurriculumVitaeReferences { get; set; } = [];
        public IList<Guid> PreferredPositionsInnerIds { get; set; } = [];
        public IList<Guid> PreferredPositionsTeamIds { get; set; } = [];
        public IList<byte> PreferredPartsInner { get; set; } = [];
        public IList<byte> PreferredPartsTeam { get; set; } = [];
        public IList<SelectValueDto> Documents { get; set; } = [];
        public MusicianProfileDeactivationDto Deactivation { get; set; }
        public IList<RegionPreferenceDto> RegionPreferencesRehearsal { get; set; } = [];
        public IList<RegionPreferenceDto> RegionPreferencesPerformance { get; set; } = [];
    }

    public class MusicianProfileDtoMappingProfile : Profile
    {
        public MusicianProfileDtoMappingProfile()
        {
            _ = CreateMap<MusicianProfile, MusicianProfileDto>()
                .ForMember(dest => dest.IsMainProfile, opt => opt.MapFrom(src => src.IsMainProfile))
                .ForMember(dest => dest.Deactivation, opt => opt.MapFrom(src => src.Deactivation))

                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.LevelAssessmentInner))
                .ForMember(dest => dest.LevelAssessmentTeam, opt => opt.MapFrom(src => src.LevelAssessmentTeam))
                .ForMember(dest => dest.ProfilePreferenceInner, opt => opt.MapFrom(src => src.ProfilePreferenceInner))
                .ForMember(dest => dest.ProfilePreferenceTeam, opt => opt.MapFrom(src => src.ProfilePreferenceTeam))

                .ForMember(dest => dest.BackgroundInner, opt => opt.MapFrom(src => src.BackgroundInner))
                .ForMember(dest => dest.BackgroundTeam, opt => opt.MapFrom(src => src.BackgroundTeam))
                .ForMember(dest => dest.SalaryComment, opt => opt.MapFrom(src => src.SalaryComment))

                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.Instrument, opt => opt.MapFrom(src => src.Instrument))
                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.QualificationId))
                .ForMember(dest => dest.SalaryId, opt => opt.MapFrom(src => src.SalaryId))
                .ForMember(dest => dest.InquiryStatusInner, opt => opt.MapFrom(src => src.InquiryStatusInner))
                .ForMember(dest => dest.InquiryStatusTeam, opt => opt.MapFrom(src => src.InquiryStatusTeam))

                .ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.DoublingInstruments))
                .ForMember(dest => dest.PreferredPositionsInnerIds, opt => opt.MapFrom(src => src.PreferredPositionsInner.Select(p => p.SelectValueSectionId)))
                .ForMember(dest => dest.PreferredPositionsTeamIds, opt => opt.MapFrom(src => src.PreferredPositionsTeam.Select(p => p.SelectValueSectionId)))
                .ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.PreferredPartsInner))
                .ForMember(dest => dest.PreferredPartsTeam, opt => opt.MapFrom(src => src.PreferredPartsTeam))
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents.Select(d => d.SelectValueMapping)))
                .ForMember(dest => dest.RegionPreferencesPerformance, opt => opt.MapFrom(src => src.RegionPreferences.Where(p => p.Type == RegionPreferenceType.Performance)))
                .ForMember(dest => dest.RegionPreferencesRehearsal, opt => opt.MapFrom(src => src.RegionPreferences.Where(p => p.Type == RegionPreferenceType.Rehearsal)))

                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
