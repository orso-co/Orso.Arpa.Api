using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.CurriculumVitaeReferenceApplication;
using Orso.Arpa.Application.DoublingInstrumentApplication;
using Orso.Arpa.Application.EducationApplication;
using Orso.Arpa.Application.General;
using Orso.Arpa.Application.MusicianProfileDeactivationApplication;
using Orso.Arpa.Application.SelectValueApplication;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MusicianProfileApplication
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
        public Guid InstrumentId { get; set; }
        public Guid? QualificationId { get; set; }
        public Guid? SalaryId { get; set; }
        public Guid? InquiryStatusInnerId { get; set; }
        public Guid? InquiryStatusTeamId { get; set; }
        public IList<DoublingInstrumentDto> DoublingInstruments { get; set; } = new List<DoublingInstrumentDto>();
        public IList<EducationDto> Educations { get; set; } = new List<EducationDto>();
        public IList<CurriculumVitaeReferenceDto> CurriculumVitaeReferences { get; set; } = new List<CurriculumVitaeReferenceDto>();
        public IList<Guid> PreferredPositionsInnerIds { get; set; } = new List<Guid>();
        public IList<Guid> PreferredPositionsTeamIds { get; set; } = new List<Guid>();
        public IList<byte> PreferredPartsInner { get; set; } = new List<byte>();
        public IList<byte> PreferredPartsTeam { get; set; } = new List<byte>();
        public IList<SelectValueDto> Documents { get; set; } = new List<SelectValueDto>();
        public MusicianProfileDeactivationDto Deactivation { get; set; }
    }

    public class MusicianProfileDtoMappingProfile : Profile
    {
        public MusicianProfileDtoMappingProfile()
        {
            CreateMap<MusicianProfile, MusicianProfileDto>()
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
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.InstrumentId))
                .ForMember(dest => dest.QualificationId, opt => opt.MapFrom(src => src.QualificationId))
                .ForMember(dest => dest.SalaryId, opt => opt.MapFrom(src => src.SalaryId))
                .ForMember(dest => dest.InquiryStatusInnerId, opt => opt.MapFrom(src => src.InquiryStatusInnerId))
                .ForMember(dest => dest.InquiryStatusTeamId, opt => opt.MapFrom(src => src.InquiryStatusTeamId))

                .ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.DoublingInstruments))
                .ForMember(dest => dest.PreferredPositionsInnerIds, opt => opt.MapFrom(src => src.PreferredPositionsInner.Select(p => p.SelectValueSectionId)))
                .ForMember(dest => dest.PreferredPositionsTeamIds, opt => opt.MapFrom(src => src.PreferredPositionsTeam.Select(p => p.SelectValueSectionId)))
                .ForMember(dest => dest.PreferredPartsInner, opt => opt.MapFrom(src => src.PreferredPartsInner))
                .ForMember(dest => dest.PreferredPartsTeam, opt => opt.MapFrom(src => src.PreferredPartsTeam))
                .ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.Documents.Select(d => d.SelectValueMapping)))

                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
