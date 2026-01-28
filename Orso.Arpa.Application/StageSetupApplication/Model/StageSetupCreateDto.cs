using System;
using AutoMapper;
using Orso.Arpa.Domain.StageSetupDomain.Commands;

namespace Orso.Arpa.Application.StageSetupApplication.Model
{
    public class StageSetupCreateDto
    {
        public string Name { get; set; }
        public int CanvasWidth { get; set; } = 1920;
        public int CanvasHeight { get; set; } = 1080;
        public bool IsActive { get; set; }
        public bool IsVisibleToPerformers { get; set; }
    }

    public class StageSetupCreateDtoMappingProfile : Profile
    {
        public StageSetupCreateDtoMappingProfile()
        {
            CreateMap<StageSetupCreateDto, CreateStageSetup.Command>()
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                .ForMember(dest => dest.FileName, opt => opt.Ignore())
                .ForMember(dest => dest.StoragePath, opt => opt.Ignore())
                .ForMember(dest => dest.ContentType, opt => opt.Ignore())
                .ForMember(dest => dest.FileSize, opt => opt.Ignore());
        }
    }
}
