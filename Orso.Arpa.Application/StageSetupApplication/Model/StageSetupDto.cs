using System;
using System.Collections.Generic;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Application.StageSetupApplication.Model
{
    public class StageSetupDto : BaseEntityDto
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisibleToPerformers { get; set; }
        public ICollection<StageSetupPositionDto> Positions { get; set; } = new List<StageSetupPositionDto>();
    }

    public class StageSetupDtoMappingProfile : Profile
    {
        public StageSetupDtoMappingProfile()
        {
            CreateMap<StageSetup, StageSetupDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();
        }
    }
}
