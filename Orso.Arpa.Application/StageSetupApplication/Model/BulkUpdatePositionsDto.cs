using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Domain.StageSetupDomain.Commands;

namespace Orso.Arpa.Application.StageSetupApplication.Model
{
    public class BulkPositionDataDto
    {
        public Guid MusicianProfileId { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public int? Row { get; set; }
        public int? Stand { get; set; }
    }

    public class BulkUpdatePositionsDto
    {
        public List<BulkPositionDataDto> Positions { get; set; } = new();
    }

    public class BulkUpdatePositionsDtoMappingProfile : Profile
    {
        public BulkUpdatePositionsDtoMappingProfile()
        {
            CreateMap<BulkPositionDataDto, BulkUpdateStageSetupPositions.PositionData>();
            CreateMap<BulkUpdatePositionsDto, BulkUpdateStageSetupPositions.Command>()
                .ForMember(dest => dest.StageSetupId, opt => opt.Ignore());
        }
    }
}
