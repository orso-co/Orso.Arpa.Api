using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Appointments.SetVenue;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentSetVenueDto
    {
        public Guid Id { get; set; }
        public Guid? VenueId { get; set; }
    }

    public class AppointmentSetVenueDtoMappingProfile : Profile
    {
        public AppointmentSetVenueDtoMappingProfile()
        {
            CreateMap<AppointmentSetVenueDto, Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.VenueId, opt => opt.MapFrom(src => src.VenueId));
        }
    }

    public class AppointmentSetVenueDtoValidator : AbstractValidator<AppointmentSetVenueDto>
    {
        public AppointmentSetVenueDtoValidator()
        {
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
        }
    }
}
