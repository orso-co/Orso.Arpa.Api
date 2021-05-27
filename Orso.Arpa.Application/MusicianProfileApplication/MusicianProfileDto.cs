using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MusicianProfileApplication
{
    public class MusicianProfileDto : BaseEntityDto
    {
        public bool IsMainProfile { get; set; }
        public bool IsDeactivated { get; set; }

        public byte LevelAssessmentPerformer { get; set; }
        public byte LevelAssessmentStaff { get; set; }
        public byte ProfilePreferencePerformer { get; set; }
        public byte ProfilePreferenceStaff { get; set; }

        public string BackgroundPerformer { get; set; }
        public string BackgroundStaff { get; set; }
        public string SalaryComment { get; set; }
        public Guid PersonId { get; set; }
        public Guid InstrumentId { get; set; }
        public Guid? QualificationId { get; set; }
        public Guid? SalaryId { get; set; }
        public Guid? InquiryStatusPerformerId { get; set; }
        public Guid? InquiryStatusStaffId { get; set; }
        public IList<DoublingInstrumentDto> DoublingInstruments { get; set; } = new List<DoublingInstrumentDto>();
        public IList<Guid> PreferredPositionsPerformerIds { get; set; } = new List<Guid>();
        public IList<Guid> PreferredPositionsStaffIds { get; set; } = new List<Guid>();
        public IList<byte> PreferredPartsPerformer { get; set; } = new List<byte>();
        public IList<byte> PreferredPartsStaff { get; set; } = new List<byte>();
    }

    public class DoublingInstrumentDto : BaseEntityDto
    {
        public Guid InstrumentId { get; set; }
        public byte LevelAssessmentPerformer { get; set; }
        public byte LevelAssessmentStaff { get; set; }
        public Guid? AvailabilityId { get; set; }
        public string Comment { get; set; }
    }

    public class MusicianProfileDtoMappingProfile : Profile
    {
        public MusicianProfileDtoMappingProfile()
        {
            CreateMap<MusicianProfile, MusicianProfileDto>()
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
                .ForMember(dest => dest.PreferredPositionsPerformerIds, opt => opt.MapFrom(src => src.PreferredPositionsPerformer.Select(p => p.SelectValueSectionId)))
                .ForMember(dest => dest.PreferredPositionsStaffIds, opt => opt.MapFrom(src => src.PreferredPositionsStaff.Select(p => p.SelectValueSectionId)))
                .ForMember(dest => dest.PreferredPartsPerformer, opt => opt.MapFrom(src => src.PreferredPartsPerformer))
                .ForMember(dest => dest.PreferredPartsStaff, opt => opt.MapFrom(src => src.PreferredPartsStaff))

                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }

    public class DoublingInstrumentDtoMappingProfile : Profile
    {
        public DoublingInstrumentDtoMappingProfile()
        {
            CreateMap<MusicianProfileSection, DoublingInstrumentDto>()
                .ForMember(dest => dest.AvailabilityId, opt => opt.MapFrom(src => src.InstrumentAvailabilityId))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.SectionId))
                .ForMember(dest => dest.LevelAssessmentPerformer, opt => opt.MapFrom(src => src.LevelAssessmentPerformer))
                .ForMember(dest => dest.LevelAssessmentStaff, opt => opt.MapFrom(src => src.LevelAssessmentStaff))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
