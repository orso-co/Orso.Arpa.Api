using System;
using AutoMapper;
using Orso.Arpa.Domain.StageSetupDomain.Commands;

namespace Orso.Arpa.Application.StageSetupApplication.Model
{
    public class StageSetupPositionCreateDto
    {
        public Guid MusicianProfileId { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public int? Row { get; set; }
        public int? Stand { get; set; }
    }

    public class StageSetupPositionCreateDtoMappingProfile : Profile
    {
        public StageSetupPositionCreateDtoMappingProfile()
        {
            CreateMap<StageSetupPositionCreateDto, CreateStageSetupPosition.Command>()
                .ForMember(dest => dest.StageSetupId, opt => opt.Ignore());
        }
    }
}
