using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Application.MusicianProfileApplication.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Application.StageSetupApplication.Model
{
    public class StageSetupPositionDto : BaseEntityDto
    {
        public Guid StageSetupId { get; set; }
        public Guid MusicianProfileId { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public int? Row { get; set; }
        public int? Stand { get; set; }

        /// <summary>
        /// Reduced musician profile info for display
        /// </summary>
        public ReducedMusicianProfileDto MusicianProfile { get; set; }
    }

    public class StageSetupPositionDtoMappingProfile : Profile
    {
        public StageSetupPositionDtoMappingProfile()
        {
            CreateMap<StageSetupPosition, StageSetupPositionDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
