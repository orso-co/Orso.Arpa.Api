using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Application.General.Model;
using Orso.Arpa.Domain.VenueDomain.Commands;

namespace Orso.Arpa.Application.RoomApplication.Model
{
    public class RoomEquipmentCreateDto : IdFromRouteDto<RoomEquipmentCreateBodyDto>
    {
    }

    public class RoomEquipmentCreateBodyDto
    {
        public Guid EquipmentId { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }
    }

    public class RoomEquipmentCreateDtoMappingProfile : Profile
    {
        public RoomEquipmentCreateDtoMappingProfile()
        {
            CreateMap<RoomEquipmentCreateDto, CreateRoomEquipment.Command>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.EquipmentId, opt => opt.MapFrom(src => src.Body.EquipmentId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Body.Quantity))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Body.Description));
        }
    }

    public class RoomEquipmentCreateDtoValidator : IdFromRouteDtoValidator<RoomEquipmentCreateDto, RoomEquipmentCreateBodyDto>
    {
        public RoomEquipmentCreateDtoValidator()
        {
            _ = RuleFor(d => d.Body)
           .SetValidator(new RoomEquipmentCreateBodyDtoValidator());
        }
    }

    public class RoomEquipmentCreateBodyDtoValidator : AbstractValidator<RoomEquipmentCreateBodyDto>
    {
        public RoomEquipmentCreateBodyDtoValidator()
        {
            RuleFor(c => c.EquipmentId)
                .NotEmpty();
            RuleFor(c => c.Quantity)
                .GreaterThanOrEqualTo(0)
                .When(c => c.Quantity.HasValue);
            RuleFor(c => c.Description)
                .RestrictedFreeText(500);
        }
    }
}