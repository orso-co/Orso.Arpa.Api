using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;

namespace Orso.Arpa.Application.RoomApplication.Model
{
    public class RoomEquipmentModifyDto : IdFromRouteDto<RoomEquipmentModifyBodyDto>
    {
    }

    public class RoomEquipmentModifyBodyDto
    {
        public int? Quantity { get; set; }
        public string Description { get; set; }
    }

    public class RoomEquipmentModifyDtoMappingProfile : Profile
    {
        public RoomEquipmentModifyDtoMappingProfile()
        {
            CreateMap<RoomEquipmentModifyDto, ModifyRoomEquipment.Command>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Body.Quantity))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description));
        }
    }

    public class RoomEquipmentModifyDtoValidator : IdFromRouteDtoValidator<RoomEquipmentModifyDto, RoomEquipmentModifyBodyDto>
    {
        public RoomEquipmentModifyDtoValidator()
        {
            _ = RuleFor(d => d.Body)
           .SetValidator(new RoomEquipmentModifyBodyDtoValidator());
        }
    }

    public class RoomEquipmentModifyBodyDtoValidator : AbstractValidator<RoomEquipmentModifyBodyDto>
    {
        public RoomEquipmentModifyBodyDtoValidator()
        {
            RuleFor(c => c.Quantity)
                .GreaterThanOrEqualTo(0)
                .When(c => c.Quantity.HasValue);
            RuleFor(c => c.Description)
                .RestrictedFreeText(500);
        }
    }
}