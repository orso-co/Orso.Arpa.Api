using System;
using AutoMapper;
using FluentValidation;

namespace Orso.Arpa.Application.Logic.Appointments
{
    public static class AddRoom
    {
        public class Dto
        {
            public Guid Id { get; set; }

            public Guid RoomId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Dto, Domain.Logic.Appointments.AddRoom.Command>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId));
            }
        }

        public class Validator : AbstractValidator<Dto>
        {
            public Validator()
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
}
