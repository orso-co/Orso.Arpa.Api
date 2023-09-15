using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.AppointmentApplication.Model
{
    public class AppointmentSetVenueDto
    {
        public Guid Id { get; set; }
        public Guid VenueId { get; set; }
    }

    public class AppointmentSetVenueDtoMappingProfile : Profile
    {
        public AppointmentSetVenueDtoMappingProfile()
        {
            CreateMap<AppointmentSetVenueDto, SetVenue.Command>()
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
            RuleFor(d => d.VenueId)
                .NotEmpty();
        }
    }
}
