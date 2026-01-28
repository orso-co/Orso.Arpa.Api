using System;
using AutoMapper;
using Orso.Arpa.Domain.StageSetupDomain.Commands;

namespace Orso.Arpa.Application.StageSetupApplication.Model
{
    public class StageSetupPositionModifyDto
    {
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public int? Row { get; set; }
        public int? Stand { get; set; }
    }

    public class StageSetupPositionModifyBodyDto : StageSetupPositionModifyDto
    {
        public Guid Id { get; set; }
        public Guid StageSetupId { get; set; }
    }

    public class StageSetupPositionModifyDtoMappingProfile : Profile
    {
        public StageSetupPositionModifyDtoMappingProfile()
        {
            CreateMap<StageSetupPositionModifyBodyDto, ModifyStageSetupPosition.Command>();
        }
    }
}
