using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.Logic.Appointments;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentAddRoomDto
    {
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }
    }

    public class AppointmentAddRoomDtoMappingProfile : Profile
    {
        public AppointmentAddRoomDtoMappingProfile()
        {
            CreateMap<AppointmentAddRoomDto, AddRoom.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId));
        }
    }

    public class AppointmentAddRoomDtoValidator : AbstractValidator<AppointmentAddRoomDto>
    {
        public AppointmentAddRoomDtoValidator()
        {
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.RoomId)
                .NotEmpty();
        }
    }
}
