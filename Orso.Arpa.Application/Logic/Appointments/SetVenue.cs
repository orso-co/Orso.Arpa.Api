using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Appointments.SetVenue;

namespace Orso.Arpa.Application.Logic.Appointments
{
    public static class SetVenue
    {
        public class Dto
        {
            public Guid Id { get; set; }
            public Guid? VenueId { get; set; }
        }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Dto, Command>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.VenueId, opt => opt.MapFrom(src => src.VenueId));
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
            }
        }
    }
}
