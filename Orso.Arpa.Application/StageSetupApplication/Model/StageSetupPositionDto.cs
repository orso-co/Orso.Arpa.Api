using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Application.PersonApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Application.StageSetupApplication.Model
{
    /// <summary>
    /// Simplified person info for stage setup display
    /// </summary>
    public class StageSetupPersonDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
    }

    /// <summary>
    /// Simplified musician profile for stage setup display
    /// </summary>
    public class StageSetupMusicianProfileDto
    {
        public Guid Id { get; set; }
        public string InstrumentName { get; set; }
        public string Qualification { get; set; }
        public StageSetupPersonDto Person { get; set; }
    }

    public class StageSetupPositionDto : BaseEntityDto
    {
        public Guid StageSetupId { get; set; }
        public Guid MusicianProfileId { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public int? Row { get; set; }
        public int? Stand { get; set; }

        /// <summary>
        /// Simplified musician profile info for display
        /// </summary>
        public StageSetupMusicianProfileDto MusicianProfile { get; set; }
    }

    public class StageSetupPositionDtoMappingProfile : Profile
    {
        public StageSetupPositionDtoMappingProfile()
        {
            CreateMap<StageSetupPosition, StageSetupPositionDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>()
                .ForMember(dest => dest.MusicianProfile, opt => opt.MapFrom(src => src.MusicianProfile));

            CreateMap<Domain.MusicianProfileDomain.Model.MusicianProfile, StageSetupMusicianProfileDto>()
                .ForMember(dest => dest.InstrumentName, opt => opt.MapFrom(src => src.Instrument != null ? src.Instrument.Name : null))
                .ForMember(dest => dest.Qualification, opt => opt.MapFrom(src => src.Qualification != null && src.Qualification.SelectValue != null ? src.Qualification.SelectValue.Name : null))
                .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person));

            CreateMap<Domain.PersonDomain.Model.Person, StageSetupPersonDto>();
        }
    }
}
