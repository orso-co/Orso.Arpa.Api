using System;
using AutoMapper;
using FluentValidation;
using Orso.Arpa.Application.Extensions;
using Orso.Arpa.Domain.Enums;
using static Orso.Arpa.Domain.Logic.Appointments.Create;

namespace Orso.Arpa.Application.AppointmentApplication
{
    public class AppointmentCreateDto
    {
        public Guid? CategoryId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public string PublicDetails { get; set; }
        public string InternalDetails { get; set; }
        public AppointmentStatus? Status { get; set; }
        public Guid? SalaryId { get; set; }
        public Guid? SalaryPatternId { get; set; }
        public Guid? ExpectationId { get; set; }
    }

    public class AppointmentCreateDtoMappingProfile : Profile
    {
        public AppointmentCreateDtoMappingProfile()
        {
            _ = CreateMap<AppointmentCreateDto, Command>();
        }
    }

    public class AppointmentCreateDtoValidator : AbstractValidator<AppointmentCreateDto>
    {
        public AppointmentCreateDtoValidator()
        {
            _ = RuleFor(d => d)
                .NotNull();
            _ = RuleFor(d => d.StartTime)
                .NotEmpty();
            _ = RuleFor(d => d.EndTime)
                .NotEmpty()
                .Must((dto, endTime) => endTime >= dto.StartTime)
                .WithMessage("'EndTime' must be greater than 'StartTime'");
            _ = RuleFor(d => d.Name)
                .NotEmpty()
                .FreeText(50);
            _ = RuleFor(d => d.InternalDetails)
                .RestrictedFreeText(1000);
            _ = RuleFor(d => d.PublicDetails)
                .RestrictedFreeText(1000);
            _ = RuleFor(d => d.Status)
                .IsInEnum();
        }
    }
}
