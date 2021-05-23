using System;
using AutoMapper;
using Orso.Arpa.Application.General;
using Orso.Arpa.Domain.Entities;

namespace Orso.Arpa.Application.MyMusicianProfileApplication
{
    public class MyMusicianProfileDto : BaseEntityDto
    {
        #region Native
        public bool IsMainProfile { get; set; }
        public bool IsDeactivated { get; set; }

        public byte LevelAssessmentPerformer { get; set; }
        public byte ProfilePreferencePerformer { get; set; }
        public string BackgroundPerformer { get; set; }
        #endregion

        #region Reference
        public Guid PersonId { get; set; }
        public Guid InstrumentId { get; set; }
        public Guid? InquiryStatusPerformerId { get; set; }
        #endregion

        #region Collection
        //public IList<MusicianProfileSection> DoublingInstruments { get; set; } = new List<MusicianProfileSection>();
        //public IList<MusicianProfileEducation> MyMusicianProfileEducations { get; set; } = new List<MusicianProfileEducation>();
        //public IList<PreferredPosition> PreferredPositionsPerformer { get; set; } = new List<PreferredPosition>();
        //public IList<PreferredPart> PreferredPartsPerformer { get; set; } = new List<PreferredPart>();
        #endregion
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

                //.ForMember(dest => dest.DoublingInstruments, opt => opt.MapFrom(src => src.DoublingInstruments))
                //.ForMember(dest => dest.MyMusicianProfileEducations, opt => opt.MapFrom(src => src.MusicianProfileEducations))
                //.ForMember(dest => dest.PreferredPositionsPerformer, opt => opt.MapFrom(src => src.PreferredPositionsPerformer))
                //.ForMember(dest => dest.PreferredPartsPerformer, opt => opt.MapFrom(src => src.PreferredPartsPerformer))

                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
