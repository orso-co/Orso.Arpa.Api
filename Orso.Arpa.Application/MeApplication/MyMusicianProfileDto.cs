using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MyMusicianProfileApplication
{
    public class MyMusicianProfileDto : BaseEntityDto
    {
        public bool IsMainProfile { get; set; }
        public bool IsDeactivated { get; set; }
        public byte LevelAssessmentPerformer { get; set; }
        public byte ProfilePreferencePerformer { get; set; }
        public string BackgroundPerformer { get; set; }
        public Guid PersonId { get; set; }
        public Guid InstrumentId { get; set; }
        public Guid? InquiryStatusPerformerId { get; set; }
        public IList<MyDoublingInstrumentDto> DoublingInstruments { get; set; } = new List<MyDoublingInstrumentDto>();
        public IList<Guid> PreferredPositionsPerformerIds { get; set; } = new List<Guid>();
        public IList<byte> PreferredPartsPerformer { get; set; } = new List<byte>();
    }

    public class MyDoublingInstrumentDto : BaseEntityDto
    {
        public Guid InstrumentId { get; set; }
        public byte LevelAssessmentPerformer { get; set; }
        public Guid? AvailabilityId { get; set; }
        public string Comment { get; set; }
    }

    public class MyMusicianProfileDtoMappingProfile : Profile
    {
        public MyMusicianProfileDtoMappingProfile()
        {
            CreateMap<MusicianProfile, MyMusicianProfileDto>()
                .ForMember(dest => dest.IsMainProfile, opt => opt.MapFrom(src => src.IsMainProfile))
                .ForMember(dest => dest.IsDeactivated, opt => opt.MapFrom(src => src.IsDeactivated))

                .ForMember(dest => dest.LevelAssessmentPerformer, opt => opt.MapFrom(src => src.LevelAssessmentPerformer))
                .ForMember(dest => dest.ProfilePreferencePerformer, opt => opt.MapFrom(src => src.ProfilePreferencePerformer))
                .ForMember(dest => dest.BackgroundPerformer, opt => opt.MapFrom(src => src.BackgroundPerformer))

                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.InstrumentId))
                .ForMember(dest => dest.InquiryStatusPerformerId, opt => opt.MapFrom(src => src.InquiryStatusPerformerId))

                .ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.DoublingInstruments))
                .ForMember(dest => dest.PreferredPositionsPerformerIds, opt => opt.MapFrom(src => src.PreferredPositionsPerformer
                    .Select(p => p.SelectValueSectionId)))
                .ForMember(dest => dest.PreferredPartsPerformer, opt => opt.MapFrom(src => src.PreferredPartsPerformer))

                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }

    public class MyDoublingInstrumentDtoMappingProfile : Profile
    {
        public MyDoublingInstrumentDtoMappingProfile()
        {
            CreateMap<MusicianProfileSection, MyDoublingInstrumentDto>()
                .ForMember(dest => dest.AvailabilityId, opt => opt.MapFrom(src => src.InstrumentAvailabilityId))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.SectionId))
                .ForMember(dest => dest.LevelAssessmentPerformer, opt => opt.MapFrom(src => src.LevelAssessmentPerformer))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
