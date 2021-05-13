using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General;
using static Orso.Arpa.Domain.Logic.Appointments.SetDates;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentSetDatesDto : BaseModifyDto<AppointmentSetDatesBodyDto>
    {
    }

    public class AppointmentSetDatesBodyDto
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

    public class AppointmentSetDatesDtoMappingProfile : Profile
    {
        public AppointmentSetDatesDtoMappingProfile()
        {
            CreateMap<AppointmentSetDatesDto, Command>()
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Body.StartTime))
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Body.EndTime));
        }
    }

    public class AppointmentSetDatesDtoValidator : BaseModifyDtoValidator<AppointmentSetDatesDto, AppointmentSetDatesBodyDto>
    {
        public AppointmentSetDatesDtoValidator()
        {
            RuleFor(d => d.Body)
                .SetValidator(new AppointmentSetDatesBodyDtoValidator());
        }
    }

    public class AppointmentSetDatesBodyDtoValidator : AbstractValidator<AppointmentSetDatesBodyDto>
    {
        public AppointmentSetDatesBodyDtoValidator()
        {
            RuleFor(c => c.EndTime)
                .NotNull()
                .When(c => c.StartTime == null);
            RuleFor(c => c.StartTime)
                .NotNull()
                .When(c => c.EndTime == null);
        }
    }
}
