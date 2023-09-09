using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Domain.AppointmentDomain.Commands;

namespace Orso.Arpa.Application.AppointmentApplication.Model
{
    public class AppointmentRemoveSectionDto
    {
        public Guid Id { get; set; }

        public Guid SectionId { get; set; }
    }

    public class AppointmentRemoveSectionDtoMappingProfile : Profile
    {
        public AppointmentRemoveSectionDtoMappingProfile()
        {
            CreateMap<AppointmentRemoveSectionDto, RemoveSectionFromAppointment.Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SectionId, opt => opt.MapFrom(src => src.SectionId));
        }
    }

    public class AppointmentRemoveSectionDtoValidator : AbstractValidator<AppointmentRemoveSectionDto>
    {
        public AppointmentRemoveSectionDtoValidator()
        {
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.SectionId)
                .NotEmpty();
        }
    }
}
