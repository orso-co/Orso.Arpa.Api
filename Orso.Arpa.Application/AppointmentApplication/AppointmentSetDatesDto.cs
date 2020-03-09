using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Interfaces;
using static Orso.Arpa.Domain.Logic.Appointments.SetDates;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentSetDatesDto : IModifyDto
    {
        public Guid Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class AppointmentSetDatesDtoMappingProfile : Profile
    {
        public AppointmentSetDatesDtoMappingProfile()
        {
            CreateMap<AppointmentSetDatesDto, Command>();
        }
    }

    public class AppointmentSetDatesDtoValidator : AbstractValidator<AppointmentSetDatesDto>
    {
        public AppointmentSetDatesDtoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(d => d)
                .NotNull();
            RuleFor(c => c.EndTime)
                    .NotNull()
                    .When(c => c.StartTime == null);
            RuleFor(c => c.StartTime)
                .NotNull()
                .When(c => c.EndTime == null);
            RuleFor(d => d.Id)
                .NotEmpty();
        }
    }
}
