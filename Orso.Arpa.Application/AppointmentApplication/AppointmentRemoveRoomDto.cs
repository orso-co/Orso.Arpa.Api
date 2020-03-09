using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Appointments.RemoveRoom;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentRemoveRoomDto
    {
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }
    }

    public class AppointmentRemoveRoomDtoMappingProfile : Profile
    {
        public AppointmentRemoveRoomDtoMappingProfile()
        {
            CreateMap<AppointmentRemoveRoomDto, Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId));
        }
    }

    public class AppointmentRemoveRoomDtoValidator : AbstractValidator<AppointmentRemoveRoomDto>
    {
        public AppointmentRemoveRoomDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.RoomId)
                .NotEmpty();
        }
    }
}
