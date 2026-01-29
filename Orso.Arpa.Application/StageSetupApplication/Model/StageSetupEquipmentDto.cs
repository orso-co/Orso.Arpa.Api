using System;
using AutoMapper;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.General.Model;
using Orso.Arpa.Domain.StageSetupDomain.Commands;
using Orso.Arpa.Domain.StageSetupDomain.Model;

namespace Orso.Arpa.Application.StageSetupApplication.Model
{
    public class StageSetupEquipmentDto : BaseEntityDto
    {
        public Guid StageSetupId { get; set; }
        public string EquipmentId { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double Rotation { get; set; }
    }

    public class StageSetupEquipmentCreateDto
    {
        public string EquipmentId { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double Rotation { get; set; }
    }

    public class StageSetupEquipmentModifyDto
    {
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double Rotation { get; set; }
    }

    public class BulkUpdateEquipmentDto
    {
        public BulkEquipmentItem[] Equipment { get; set; }
    }

    public class BulkEquipmentItem
    {
        public Guid Id { get; set; }
        public string EquipmentId { get; set; }
        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double Rotation { get; set; }
    }

    public class StageSetupEquipmentDtoMappingProfile : Profile
    {
        public StageSetupEquipmentDtoMappingProfile()
        {
            CreateMap<StageSetupEquipment, StageSetupEquipmentDto>()
                .IncludeBase<BaseEntity, BaseEntityDto>();

            CreateMap<StageSetupEquipmentCreateDto, CreateStageSetupEquipment.Command>();
        }
    }
}
