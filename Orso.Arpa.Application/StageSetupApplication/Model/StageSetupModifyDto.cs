using System;
using AutoMapper;
using Orso.Arpa.Domain.StageSetupDomain.Commands;

namespace Orso.Arpa.Application.StageSetupApplication.Model
{
    public class StageSetupModifyDto
    {
        public string Name { get; set; }
        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisibleToPerformers { get; set; }
    }

    public class StageSetupModifyBodyDto : StageSetupModifyDto
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
    }

    public class StageSetupModifyDtoMappingProfile : Profile
    {
        public StageSetupModifyDtoMappingProfile()
        {
            CreateMap<StageSetupModifyBodyDto, ModifyStageSetup.Command>();
        }
    }
}
