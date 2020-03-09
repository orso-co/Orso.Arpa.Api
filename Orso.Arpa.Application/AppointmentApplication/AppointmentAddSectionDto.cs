using System;
using AutoMapper;
using FluentValidation;
using static Orso.Arpa.Domain.Logic.Appointments.AddSection;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentAddSectionDto
    {
        public Guid Id { get; set; }

        public Guid SectionId { get; set; }
    }

    public class AppointmentAddSectionDtoMappingProfile : Profile
    {
        public AppointmentAddSectionDtoMappingProfile()
        {
            CreateMap<AppointmentAddSectionDto, Command>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SectionId, opt => opt.MapFrom(src => src.SectionId));
        }
    }

    public class AppointmentAddSectionDtoValidator : AbstractValidator<AppointmentAddSectionDto>
    {
        public AppointmentAddSectionDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(d => d.Id)
                .NotEmpty();
            RuleFor(d => d.SectionId)
                .NotEmpty();
        }
    }
}
