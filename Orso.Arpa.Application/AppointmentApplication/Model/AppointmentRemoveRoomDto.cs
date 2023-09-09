using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.AppointmentApplication.Model
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
            CreateMap<AppointmentRemoveRoomDto, RemoveRoom.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId));
        }
    }

    public class AppointmentRemoveRoomDtoValidator : AbstractValidator<AppointmentRemoveRoomDto>
    {
        public AppointmentRemoveRoomDtoValidator()
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
