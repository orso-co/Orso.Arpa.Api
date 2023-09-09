using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.MusicianProfileDomain.Model;

namespace Orso.Arpa.Application.DoublingInstrumentApplication.Model
{
    public class DoublingInstrumentDto : BaseEntityDto
    {
        public Guid InstrumentId { get; set; }
        public byte LevelAssessmentInner { get; set; }
        public byte LevelAssessmentTeam { get; set; }
        public Guid? AvailabilityId { get; set; }
        public string Comment { get; set; }
    }

    public class DoublingInstrumentDtoMappingProfile : Profile
    {
        public DoublingInstrumentDtoMappingProfile()
        {
            CreateMap<MusicianProfileSection, DoublingInstrumentDto>()
                .ForMember(dest => dest.AvailabilityId, opt => opt.MapFrom(src => src.InstrumentAvailabilityId))
                .ForMember(dest => dest.InstrumentId, opt => opt.MapFrom(src => src.SectionId))
                .ForMember(dest => dest.LevelAssessmentInner, opt => opt.MapFrom(src => src.LevelAssessmentInner))
                .ForMember(dest => dest.LevelAssessmentTeam, opt => opt.MapFrom(src => src.LevelAssessmentTeam))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
