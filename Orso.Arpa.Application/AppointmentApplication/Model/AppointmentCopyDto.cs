using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.General.Extensions;
using Orso.Arpa.Domain.AppointmentDomain.Commands;
using Orso.Arpa.Domain.AppointmentDomain.Enums;

namespace Orso.Arpa.Application.AppointmentApplication.Model
{
    public class AppointmentCopyDto
    {
        public Guid AppointmentIdToCopy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class AppointmentCopyDtoMappingProfile : Profile
    {
        public AppointmentCopyDtoMappingProfile()
        {
            _ = CreateMap<AppointmentCopyDto, CopyAppointment.Command>();
        }
    }

    public class AppointmentCopyDtoValidator : AbstractValidator<AppointmentCopyDto>
    {
        public AppointmentCopyDtoValidator()
        {
            _ = RuleFor(d => d)
                .NotNull();
            _ = RuleFor(d => d.StartTime)
                .NotEmpty();
            _ = RuleFor(d => d.EndTime)
                .NotEmpty()
                .Must((dto, endTime) => endTime >= dto.StartTime)
                .WithMessage("'EndTime' must be greater than 'StartTime'");
            _ = RuleFor(d => d.AppointmentIdToCopy)
                .NotEmpty();
        }
    }
}
